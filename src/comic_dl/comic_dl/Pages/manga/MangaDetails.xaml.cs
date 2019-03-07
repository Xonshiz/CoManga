using comic_dl._0_manga.jsonStructure;
using comic_dl._manga.downloader;
using comic_dl._manga.jsonStructure;
using comic_dl.commonFunctionalities;
using comic_dl.interace;
using comic_dl.internalData;
using comic_dl.internalData.manga;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Refit;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace comic_dl.Pages.manga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MangaDetails : ContentPage
    {
        string mangaOid;
        IMangaRockAPI mangaRockAPI;

        MangaInformationRootObject mangaInformationRootObject;

        public MangaDetails(string mangaOid)
        {
            InitializeComponent();
            this.mangaOid = mangaOid;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await MangaDetailsFetcher();
            this.Title = mangaInformationRootObject.data.name;
            showTitle.Text = this.Title;

            authorName.Text = mangaInformationRootObject.data.author;
            totalChapters.Text = Convert.ToString(mangaInformationRootObject.data.total_chapters);
            description.Text = CommonFunctions.TruncateAtWord(mangaInformationRootObject.data.description, 100);

            string bgImage = "";

            if (!string.IsNullOrEmpty(mangaInformationRootObject.data.cover))
                bgImage = mangaInformationRootObject.data.cover;
            else if (!string.IsNullOrEmpty(mangaInformationRootObject.data.thumbnail))
                bgImage = mangaInformationRootObject.data.thumbnail;
            else if (!string.IsNullOrEmpty(mangaInformationRootObject.data.artworks[0]))
                bgImage = mangaInformationRootObject.data.artworks[0];

            myBackgroundImage.Source = ImageSource.FromUri(new Uri(bgImage));
            MangaChapters_List.ItemsSource = mangaInformationRootObject.data.chapters;
            
        }

        private async Task MangaDetailsFetcher()
        {
            try
            {
                progressBar.IsRunning = true;
                progressBar.IsVisible = true;
                mangaRockAPI = RestService.For<IMangaRockAPI>(ServiceUrl.base_manga_url);
                mangaInformationRootObject = await mangaRockAPI.GetMangaDetails(this.mangaOid);
                mangaInformationRootObject.data.chapters.Reverse();
                CommonInternalData.isMangaSelected = true;
                progressBar.IsRunning = false;
                progressBar.IsVisible = false;

            }
            catch (Exception LatestUpdateFetchingException)
            {
                Crashes.TrackError(LatestUpdateFetchingException);
                await DisplayAlert("Dead", "Exception Occurred. Please Try Again After Some Time.", "ok");
            }
        }

        private async void MangaChapters_Refreshing(object sender, EventArgs e)
        {
            await MangaDetailsFetcher();
            MangaChapters_List.ItemsSource = mangaInformationRootObject.data.chapters;
        }

        private void MangaChapters_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MangaChapters_List.ItemSelected += null;
        }

        private async void DownloadButton_Clicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button) sender;
            item.IsEnabled = false;

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await DisplayAlert("Need Storage Permission", "Need ", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];
                }

                if (status == PermissionStatus.Granted)
                {
                    await DownloadHandler(item);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Storage Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Exception", "Couldn't Take Permission", "ok");
            }

        }

        private async Task DownloadHandler(Xamarin.Forms.Button item)
        {
            MangaInformationChapter mangaInformationChapter = mangaInformationRootObject.data.chapters.Find(r => r.oid == item.CommandParameter.ToString());
            ListOfPagesRootObject listOfPagesRootObject = await mangaRockAPI.GetMangaPages(item.CommandParameter.ToString());
            Downloader downloader = new Downloader();

            bool isDownloadFinished = await downloader.FilesDownloader(listOfPagesRootObject.data, this.Title, mangaInformationChapter.order.ToString(), CommonInternalData.DownloadType.MangaRock, item);
            item.Text = "Download";
            item.IsEnabled = true;

            if (isDownloadFinished)
            {
                await DisplayAlert("Downloaded", CurrentData.response, "ok");
            }
            else
                await DisplayAlert("Failed", CurrentData.response, "ok");
            return;
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
    }
}