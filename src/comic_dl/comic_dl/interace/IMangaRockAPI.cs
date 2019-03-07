using comic_dl._0_manga.jsonStructure;
using comic_dl._manga.jsonStructure;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace comic_dl.interace
{
    [Headers("User-Agent: :request:")]
    interface IMangaRockAPI
    {
        [Get("/query/web401/info?oid={oid}")]
        Task<MangaInformationRootObject> GetMangaDetails(string oid);

        [Get("/query/web401/pages?oid={oid}")]
        Task<ListOfPagesRootObject> GetMangaPages(string oid);

        [Get("")]
        Task<HttpResponseMessage> GetPageImage();

        [Post("/query/web401/mrs_search")]
        Task<SearchResult> GetSearchResults([Body] string body);

        [Post("/meta")]
        Task<SearchResultDetails> GetSearchResultDetails([Body] string body);
    }
}
