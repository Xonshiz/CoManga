using comic_dl._comic.downloader;
using comic_dl._comic.sourceStructure;
using comic_dl.internalData;
using comic_dl.internalData.comic;
using comic_dl.parsers;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace comic_dl.Pages.comic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ComicDetails : ContentPage
	{
        private string _comicUrl;
        private ReadComicBooksOnlineLatestItems_Parser _parser;
        ComicDetailStructure comicDetailStructure;
        ComicImagesStructure comicImagesStructure;


        public ComicDetails (string comicUrl)
		{
			InitializeComponent ();
            this._comicUrl = comicUrl;
            _parser = new ReadComicBooksOnlineLatestItems_Parser();
            comicDetailStructure = new ComicDetailStructure();
            comicImagesStructure = new ComicImagesStructure();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ComicDetailsFetcher();
            this.Title = comicDetailStructure.comicTitle;

            showTitle.Text = this.Title;

            authorName.Text = comicDetailStructure.comicAuthors;
            genresComic.Text = comicDetailStructure.comicGenre;
            description.Text = comicDetailStructure.comicDescription;
            myBackgroundImage.Source = ImageSource.FromUri(new Uri(comicDetailStructure.comicImage));

            ComicChapters_List.ItemsSource = comicDetailStructure.comicChapterList;
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        private async Task ComicDetailsFetcher()
        {
            try
            {
                progressBar.IsRunning = true;
                progressBar.IsVisible = true;

                comicDetailStructure = await _parser.GetComicDetails(_comicUrl);
                progressBar.IsRunning = false;
                progressBar.IsVisible = false;
                CommonInternalData.isMangaSelected = false;
                CommonInternalData.isComicSelected = true;
            }
            catch (Exception LatestUpdateFetchingException)
            {
                Crashes.TrackError(LatestUpdateFetchingException);
                await DisplayAlert("Dead", "Exception Occurred. Please Try Again After Some Time.", "ok");
            }
        }

        private async void ComicChapters_Refreshing(object sender, EventArgs e)
        {
            await ComicDetailsFetcher();
            ComicChapters_List.ItemsSource = comicDetailStructure.comicChapterList;
        }

        private void ComicChapters_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ComicChapters_List.ItemSelected += null;
        }

        private async void ComicDownloadButton_Clicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            //item.Text = "Downloading";
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
            ComicChapterList comicInformationChapter = comicDetailStructure.comicChapterList.Where(r => r.chapterLink == item.CommandParameter.ToString()).FirstOrDefault();
            comicImagesStructure = await _parser.GetComicChapterImages(comicInformationChapter.chapterLink);
            if (comicImagesStructure.comicChapterImageLink.Count > 0)
            {
                ComicDownloader downloader = new ComicDownloader();
                bool isDownloadFinished = await downloader.FilesDownloader(comicImagesStructure.comicChapterImageLink, this.Title, comicImagesStructure.comicChapterNumber.ToString(), CommonInternalData.DownloadType.RCO, item);
                item.Text = "Download";
                item.IsEnabled = true;

                if (isDownloadFinished)
                {
                    await DisplayAlert("Downloaded", CurrentData.comic_response, "ok");
                }
                else
                    await DisplayAlert("Failed", CurrentData.comic_response, "ok");
            }
            else
            {
                await DisplayAlert("Hello", "Chapter Not Released Yet.", "ok");
            }
            return;
        }
    }
}