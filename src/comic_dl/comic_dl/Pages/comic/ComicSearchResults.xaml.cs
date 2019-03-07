using comic_dl._comic.sourceStructure;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace comic_dl.Pages.comic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ComicSearchResults : ContentPage
	{
        ObservableCollection<ComicListStructure> _comicSearchResults = new ObservableCollection<ComicListStructure> { };

        public ComicSearchResults (string searchTitle, ObservableCollection<ComicListStructure> comicSearchResults)
		{
			InitializeComponent ();
            this.Title = "Searches For : " + searchTitle;
            _comicSearchResults = comicSearchResults;
            ComicSearchResult_List.ItemsSource = _comicSearchResults;
		}

        private async void ComicSearchResult_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedRequest = e.SelectedItem as ComicListStructure;
            await Navigation.PushAsync(new ComicDetails(selectedRequest.comicLink));
            ComicSearchResult_List.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
    }
}