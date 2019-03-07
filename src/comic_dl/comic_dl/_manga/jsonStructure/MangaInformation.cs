using System.Collections.Generic;

namespace comic_dl._0_manga.jsonStructure
{
    public class MangaInformationChapter
    {
        public int cid { get; set; }
        public string oid { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public int updatedAt { get; set; }
    }

    public class MangaInformationAuthor
    {
        public string oid { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public string role { get; set; }
    }

    public class MangaInformationRichCategory
    {
        public string oid { get; set; }
        public string name { get; set; }
    }

    public class MangaInformationExtra
    {
        public string Published { get; set; }
        public string Serialization { get; set; }
    }

    public class MangaInformationData
    {
        public int mid { get; set; }
        public string oid { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public int rank { get; set; }
        public int msid { get; set; }
        public bool completed { get; set; }
        public int last_update { get; set; }
        public bool removed { get; set; }
        public int direction { get; set; }
        public int total_chapters { get; set; }
        public string description { get; set; }
        public List<int> categories { get; set; }
        public List<MangaInformationChapter> chapters { get; set; }
        public string thumbnail { get; set; }
        public string cover { get; set; }
        public List<string> artworks { get; set; }
        public List<string> alias { get; set; }
        public List<object> characters { get; set; }
        public List<MangaInformationAuthor> authors { get; set; }
        public List<MangaInformationRichCategory> rich_categories { get; set; }
        public MangaInformationExtra extra { get; set; }
        public string mrs_series { get; set; }
    }

    public class MangaInformationRootObject
    {
        public int code { get; set; }
        public MangaInformationData data { get; set; }
    }
}
