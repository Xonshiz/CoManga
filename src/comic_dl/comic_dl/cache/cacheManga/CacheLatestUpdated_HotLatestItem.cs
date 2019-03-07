using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace comic_dl.cache.cacheManga
{
    [Table("CacheLatestUpdated_HotLatestItem")]
    public class CacheLatestUpdated_HotLatestItem
    {
        [PrimaryKey]
        public string oid { get; set; }
        
        public string name { get; set; }

        //public List<CacheMangaGenre> genres { get; set; }
        
        public int rank { get; set; }
        
        public int updated_chapters { get; set; }
        
        //public List<CacheLatestUpdated_NewChapter2> new_chapters { get; set; }
        
        public bool completed { get; set; }
        
        public string thumbnail { get; set; }
        
        public string updated_at { get; set; }
    }

    [Table("CacheMangaGenre")]
    public class CacheMangaGenre
    {
        [PrimaryKey]
        public string oid { get; set; }

        public string genre { get; set; }
    }

    [Table("CacheNewMangaChapters")]
    public class CacheNewMangaChapters
    {
        [PrimaryKey]
        public string oid { get; set; }
        
        public string cid { get; set; } // Comic ID

        public string name { get; set; }
        
        public string updatedAt { get; set; }
    }
}
