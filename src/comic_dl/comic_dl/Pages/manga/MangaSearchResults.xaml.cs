using comic_dl._0_manga.jsonStructure;
using comic_dl._manga.jsonStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace comic_dl.Pages.manga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MangaSearchResults : ContentPage
    {
        List<Series> seriesList = new List<Series>() { };

        public MangaSearchResults(string searchTitle, Dictionary<string, Series> seriesDict )
        {
            InitializeComponent();
            this.Title = "Searches For : " + searchTitle;
            foreach (var item in seriesDict)
            {
                this.seriesList.Add(new Series {
                    author_ids = item.Value.author_ids,
                    completed = item.Value.completed,
                    name = item.Value.name,
                    oid = item.Value.oid,
                    thumbnail = item.Value.thumbnail,
                    total_chapters = item.Value.total_chapters
                });
            }
            SearchResult_List.ItemsSource = this.seriesList;
        }

        private async void SearchResult_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedRequest = e.SelectedItem as Series;
            await Navigation.PushAsync(new MangaDetails(selectedRequest.oid));
            SearchResult_List.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
    }
}