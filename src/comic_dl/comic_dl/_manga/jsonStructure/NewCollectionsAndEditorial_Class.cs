/*
 * This class fetches the list of new collection and editorial collection from mangarock's API.
 * The skeleton was generated via json2csharp.
 * API reference : https://api.mangarockhd.com/query/web400/foryou?msid=71&country=CountryName
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

// Custome Imports
using comic_dl.internalData;

namespace comic_dl._0_manga.jsonStructure
{
    class NewCollectionsAndEditorial_Class
    {
        public async static Task<NewCollection_RootObject> GetNewCollection()
        {
            var http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.bearerToken);
            var response = await http.GetAsync(ServiceUrl.manga_get_new_collection);
            var result = await response.Content.ReadAsStringAsync();
            var Serializer = new DataContractJsonSerializer(typeof(NewCollection_RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (NewCollection_RootObject)Serializer.ReadObject(ms);

            return data;
        }

    }

    public class NewCollectionTitle
    {
        public string en { get; set; }
    }

    public class NewCollectionSubtitle
    {
        public string en { get; set; }
    }

    public class NewCollectionTitle2
    {
        public string en { get; set; }
    }

    public class NewCollectionSubtitle2
    {
        public string en { get; set; }
    }

    public class NewCollectionCta
    {
        public string en { get; set; }
    }

    public class NewCollectionDatum
    {
        public string oid { get; set; }
        public NewCollectionTitle2 title { get; set; }
        public NewCollectionSubtitle2 subtitle { get; set; }
        public string banner_url { get; set; }
        public string detail_json_url { get; set; }
        public NewCollectionCta cta { get; set; }
        public List<string> items { get; set; }
        public List<string> oids { get; set; }
    }

    public class NewCollectionItems
    {
        public string component { get; set; }
        public string preview_json_url { get; set; }
        public List<NewCollectionDatum> data { get; set; }
    }

    public class NewCollectionSection
    {
        public string sectionId { get; set; }
        public NewCollectionTitle title { get; set; }
        public NewCollectionSubtitle subtitle { get; set; }
        public string type { get; set; }
        public NewCollectionItems items { get; set; }
    }

    public class NewCollectionData
    {
        public string tracking_id { get; set; }
        public List<NewCollectionSection> sections { get; set; }
    }

    public class NewCollection_RootObject
    {
        public int code { get; set; }
        public NewCollectionData data { get; set; }
    }
}
