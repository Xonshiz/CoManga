using comic_dl._comic.sourceStructure;
using comic_dl.internalData;
using comic_dl.parsers;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace comic_dl._comic
{
    class LatestComicFetcher
    {
        public async static Task<ObservableCollection<ComicListStructure>> GetLatestComicUpdates()
        {
            var http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.bearerToken);
            var response = await http.GetAsync(ServiceUrl.base_comic_url);
            var webResult = await response.Content.ReadAsStringAsync();
            ReadComicBooksOnlineLatestItems_Parser parser = new ReadComicBooksOnlineLatestItems_Parser();
            ObservableCollection<ComicListStructure> comicListStructure  = await parser.ExtractLatestComics(webResult);
            return comicListStructure;
            
        }

        public async static Task<string> GetComicDetails(string comicUrl)
        {
            var http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.bearerToken);
            var response = await http.GetAsync(comicUrl);
            var webResult = await response.Content.ReadAsStringAsync();
            return webResult;

        }

        public async static Task<string> GetComicChapterDetails(string comicChapterUrl)
        {
            var http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.bearerToken);
            var response = await http.GetAsync(comicChapterUrl);
            var webResult = await response.Content.ReadAsStringAsync();
            return webResult;

        }

        public async static Task<ObservableCollection<ComicListStructure>> GetComicSearches(string searchText)
        {
            var http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.bearerToken);
            ObservableCollection<ComicListStructure> comicListStructure = new ObservableCollection<ComicListStructure>() { };
            var response = await http.GetAsync(ServiceUrl.base_comic_search_url + searchText);
            var webResult = await response.Content.ReadAsStringAsync();
            // When Nothing is Found, we return an empty list.
            if (webResult.Contains("Your search yielded no results"))
                return comicListStructure;
            ReadComicBooksOnlineLatestItems_Parser parser = new ReadComicBooksOnlineLatestItems_Parser();
            comicListStructure = await parser.ExtractComicSearch(searchText, webResult);
            return comicListStructure;

        }

        public async static Task<string> GetComicPageContent(string pageUrl)
        {
            var http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.bearerToken);
            var response = await http.GetAsync(pageUrl);
            var webResult = await response.Content.ReadAsStringAsync();
            return webResult;

        }
    }
}
