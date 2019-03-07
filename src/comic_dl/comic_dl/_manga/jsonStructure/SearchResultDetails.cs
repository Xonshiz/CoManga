using System.Collections.Generic;

namespace comic_dl._manga.jsonStructure
{
    public class SearchResultDetails
    {
        public int code { get; set; }
        public Dictionary<string, Series> data { get; set; }
    }

    public class Series
    {
        public List<string> author_ids { get; set; }
        public bool completed { get; set; }
        public string name { get; set; }
        public string oid { get; set; }
        public string thumbnail { get; set; }
        public int total_chapters { get; set; }
    }
}
