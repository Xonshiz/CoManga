using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comic_dl.interace
{
    [Headers("User-Agent: :request:")]
    interface IReadComicOnline
    {
        //[Get("")]
        //Task<MangaInformationRootObject> GetMangaDetails(string oid);
    }
}
