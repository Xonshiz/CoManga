using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

// Custom Imports
using comic_dl.internalData;
using comic_dl.parsers;

namespace comic_dl._0_manga.jsonStructure
{
    class LatestUpdatedManga
    {
        public async static Task<LatestUpdated_RootObject> GetLatestUpdates()
        {
            var http = new HttpClient();
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.bearerToken);
            var response = await http.GetAsync(ServiceUrl.base_manga_url_website);
            var webResult = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Web Result : " + webResult);

            MangaRockMainPageJsonExractor_Parser jsonExtractor = new MangaRockMainPageJsonExractor_Parser();
            string parsedJson = await jsonExtractor.ExtractJsonFromMainPage(webResult);

            var Serializer = new DataContractJsonSerializer(typeof(LatestUpdated_RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(parsedJson));
            var data = (LatestUpdated_RootObject)Serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class LatestUpdated_NewChapter
    {
        [DataMember]
        public string oid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string updatedAt { get; set; }
    }

    [DataContract]
    public class LatestUpdated_LatestItem
    {
        [DataMember]
        public string oid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<string> genres { get; set; }
        [DataMember]
        public int rank { get; set; }
        [DataMember]
        public int updated_chapters { get; set; }
        [DataMember]
        public List<LatestUpdated_NewChapter> new_chapters { get; set; }
        [DataMember]
        public bool completed { get; set; }
        [DataMember]
        public string thumbnail { get; set; }
        [DataMember]
        public string updated_at { get; set; }
    }

    [DataContract]
    public class LatestUpdated_NewChapter2
    {
        [DataMember]
        public string oid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string updatedAt { get; set; }
    }

    [DataContract]
    public class LatestUpdated_HotLatestItem
    {
        [DataMember]
        public string oid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<string> genres { get; set; }
        [DataMember]
        public int rank { get; set; }
        [DataMember]
        public int updated_chapters { get; set; }
        [DataMember]
        public List<LatestUpdated_NewChapter2> new_chapters { get; set; }
        [DataMember]
        public bool completed { get; set; }
        [DataMember]
        public string thumbnail { get; set; }
        [DataMember]
        public string updated_at { get; set; }
    }

    [DataContract]
    public class LatestUpdated_Home
    {
        [DataMember]
        public long lastLatestFetched { get; set; }
        [DataMember]
        public List<LatestUpdated_LatestItem> latestItems { get; set; }
        [DataMember]
        public List<LatestUpdated_HotLatestItem> hotLatestItems { get; set; }
        [DataMember]
        public object error { get; set; }
    }

    [DataContract]
    public class LatestUpdated_RootObject
    {
        [DataMember]
        public LatestUpdated_Home home { get; set; }
    }
}