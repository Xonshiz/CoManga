using comic_dl._comic;
using comic_dl._comic.sourceStructure;
using comic_dl.commonFunctionalities;
using comic_dl.internalData;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace comic_dl.Pages.comic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComicListing : ContentPage
    {
        ObservableCollection<ComicListStructure> _latestComicFetcher = new ObservableCollection<ComicListStructure> { };
        ObservableCollection<ComicListStructure> _comicSearchResult = new ObservableCollection<ComicListStructure> { };
        DateTime lastLoadedTime;
        int numberOfTimePageLoaded = 0;

        public ComicListing()
        {
            InitializeComponent();
            lastLoadedTime = DateTime.Now;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            CommonInternalData.isComicSelected = true;
            CommonInternalData.isMangaSelected = false;
            if (numberOfTimePageLoaded <= 1)
                await PopularTitlesFetcher();
            if ((DateTime.Now - lastLoadedTime).Duration().TotalMinutes > 5)
                await PopularTitlesFetcher();
            PopularTitles_List.ItemsSource = _latestComicFetcher;
        }

        private async Task PopularTitlesFetcher()
        {
            try
            {
                progressBar.IsRunning = true;
                progressBar.IsVisible = true;

                _latestComicFetcher = await LatestComicFetcher.GetLatestComicUpdates();

                progressBar.IsRunning = false;
                progressBar.IsVisible = false;
            }
            catch (Exception LatestUpdateFetchingException)
            {
                //Crashes.TrackError(LatestUpdateFetchingException);
                await DisplayAlert("Dead", LatestUpdateFetchingException.ToString(), "ok");
            }
        }

        private async void PopularTitles_Refreshing(object sender, EventArgs e)
        {
            await PopularTitlesFetcher();
            PopularTitles_List.ItemsSource = _latestComicFetcher;
            PopularTitles_List.EndRefresh();
        }

        private async void PopularTitles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedRequest = e.SelectedItem as ComicListStructure;
            await Navigation.PushAsync(new ComicDetails(selectedRequest.comicLink));
            PopularTitles_List.SelectedItem = null;

        }

        private async void Handle_SearchButtonPressedAsync(object sender, System.EventArgs e)
        {
            progressBar.IsRunning = true;
            progressBar.IsVisible = true;

            ComicSearchBar.Text = ComicSearchBar.Text.Trim();
            string searchText = ComicSearchBar.Text;
            await SearchTitles(searchText);

            if (_comicSearchResult.Count <= 0)
            {
                await DisplayAlert("No Results Found!", "Sorry, Couldn't Find " + searchText + ".", "Ok");
                ComicSearchBar.Text = "";
                return;
            }
            progressBar.IsRunning = false;
            progressBar.IsVisible = false;

            await Navigation.PushAsync(new ComicSearchResults(CommonFunctions.ToUpperCase(searchText), _comicSearchResult));
            ComicSearchBar.Text = "";
            return;
            //PopularTitles_List.ItemsSource = _latestComicFetcher;
        }

        private async Task SearchTitles(string searchText)
        {
            try
            {
                _comicSearchResult = await LatestComicFetcher.GetComicSearches(searchText);

            }
            catch (Exception LatestUpdateFetchingException)
            {
                Crashes.TrackError(LatestUpdateFetchingException);
                await DisplayAlert("Dead", "Exception Occurred. Please Try Again After Some Time.", "ok");
            }
        }
    }
}