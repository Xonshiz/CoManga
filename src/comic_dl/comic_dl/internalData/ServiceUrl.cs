using System;
using System.Collections.Generic;
using System.Text;

namespace comic_dl.internalData
{
    class ServiceUrl
    {
        public static string base_manga_url_website = "https://mangarock.com/";
        public static string base_manga_url = "https://api.mangarockhd.com";
        public static string manga_get_new_collection = base_manga_url + "/query/web401/foryou?msid=71";
        public static string manga_get_latest_updates = base_manga_url + "/query/web401/mrs_latest";
        public static string manga_information = base_manga_url + "/query/web401/info?oid=";
        public static string manga_search_result = base_manga_url + "/query/web401/mrs_search";
        public static string manga_search_result_details = base_manga_url + "/meta";
        public static string base_comic_url = "https://readcomicsonline.me";
        public static string base_comic_image_url = base_comic_url + "/reader/";
        public static string base_comic_search_url = base_comic_url + "/search/node/";
    }
}
