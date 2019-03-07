using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Custom Imports
using comic_dl.internalData;
using comic_dl._0_manga.jsonStructure;
using comic_dl.Pages.manga;
using comic_dl.interace;
using Refit;
using comic_dl._manga.jsonStructure;
using comic_dl.commonFunctionalities;
using Microsoft.AppCenter.Crashes;

namespace comic_dl.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MangaListing : ContentPage
	{
        LatestUpdated_RootObject latestUpdates;
        IMangaRockAPI mangaRockAPI;
        SearchResult searchResult;
        SearchResultDetails searchResultDetails;
        DateTime lastLoadedTime;
        int numberOfTimePageLoaded = 0;

        public MangaListing ()
		{
            InitializeComponent ();
            lastLoadedTime = DateTime.Now;
            LatestUpdatedChapter_List.UserInteracted += CarouselView_UserInteracted;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            numberOfTimePageLoaded++;
            CommonInternalData.isMangaSelected = true;
            CommonInternalData.isComicSelected = false;

            if (numberOfTimePageLoaded <= 1)
                await LatestUpdatedChapterFetcher(CommonInternalData.RefreshType.HotLatestItems, true);
            
            if ((DateTime.Now - lastLoadedTime).Duration().TotalMinutes > 5)
                await LatestUpdatedChapterFetcher(CommonInternalData.RefreshType.HotLatestItems, true);

        }

        private void CarouselView_UserInteracted(PanCardView.CardsView view, PanCardView.EventArgs.UserInteractedEventArgs args)
        {
            if (args.Status == PanCardView.Enums.UserInteractionStatus.Started)
            {
                MainPage.DisableSwipe();
            }
        }

        private async Task LatestUpdatedChapterFetcher(CommonInternalData.RefreshType refreshType = CommonInternalData.RefreshType.HotLatestItems, bool refreshBoth = false)
        {
            // refreshBoth will let us fetch "Latest Items" list along with "Hot Items"
            progressBarHotItems.IsRunning = true;
            progressBarHotItems.IsVisible = true;
            
            try
            {
                latestUpdates = await LatestUpdatedManga.GetLatestUpdates();
                LatestUpdatedChapter_List.ItemsSource = latestUpdates.home.hotLatestItems;
               
                if (refreshType == CommonInternalData.RefreshType.LatestItems || refreshBoth)
                    LatestItems_List.ItemsSource = latestUpdates.home.latestItems;
                progressBarHotItems.IsRunning = false;
                progressBarHotItems.IsVisible = false;
                return;
            }
            catch (Exception LatestUpdateFetchingException)
            {
                //Console.WriteLine("LatestUpdateFetchingException : " + LatestUpdateFetchingException);
                Crashes.TrackError(LatestUpdateFetchingException);
                await DisplayAlert("Dead", "Exception Occurred. Please Try Again After Some Time.", "ok");
            }
        }
        
        private async void LatestUpdatedChapter_ItemSelected(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            await Navigation.PushAsync(new MangaDetails(item.CommandParameter.ToString()));

        }

        private async void LatestItems_List_Refreshing(object sender, EventArgs e)
        {
            await LatestUpdatedChapterFetcher(CommonInternalData.RefreshType.LatestItems);
            LatestItems_List.EndRefresh();
        }

        private async void LatestItems_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var selectedRequest = e.SelectedItem as LatestUpdated_LatestItem;
            await Navigation.PushAsync(new MangaDetails(selectedRequest.oid));
            LatestItems_List.SelectedItem = null;

        }

        private async void Handle_SearchButtonPressedAsync(object sender, System.EventArgs e)
        {
            progressBarHotItems.IsRunning = true;
            progressBarHotItems.IsVisible = true;
            MangaSearchBar.Text = MangaSearchBar.Text.Trim();
            string searchText = MangaSearchBar.Text;
            await SearchResultsFetcherManga(searchText);

            progressBarHotItems.IsRunning = false;
            progressBarHotItems.IsVisible = false;
            if (searchResultDetails.data.Count <= 0)
            {
                await DisplayAlert("No Results Found!", "Sorry, Couldn't Find " + searchText + ".", "Ok");
                MangaSearchBar.Text = "";
                return;
            }
            await Navigation.PushAsync(new MangaSearchResults(CommonFunctions.ToUpperCase(searchText), searchResultDetails.data));
            MangaSearchBar.Text = "";
            return;
        }

        private async Task SearchResultsFetcherManga(string searchText)
        {
            try
            {
                mangaRockAPI = RestService.For<IMangaRockAPI>(ServiceUrl.base_manga_url);
                Dictionary<string, string> jsonDict = new Dictionary<string, string> {
                    {"type", "series" },
                    {"keywords", searchText}
                 };
                searchResult = await mangaRockAPI.GetSearchResults(CommonFunctions.DictionaryToJson(jsonDict));
                string tempValues = CommonFunctions.ListToListString(searchResult.data);
                searchResultDetails = await mangaRockAPI.GetSearchResultDetails(tempValues);
            }
            catch (Exception LatestUpdateFetchingException)
            {
                Crashes.TrackError(LatestUpdateFetchingException);
                await DisplayAlert("Dead", "Exception Occurred. Please Try Again After Some Time.", "ok");
            }
        }
    }
}