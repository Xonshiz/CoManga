using System.Collections.Generic;

namespace comic_dl.internalData.manga
{
    class CurrentData
    {
        public static string current_selected_manga_oid { get; set; }
        public static string current_selected_manga_name { get; set; }
        public static string current_selected_manga_description { get; set; }
        public static Dictionary<string, int> current_selected_manga_series_dict { get; set; }
        public static string response { get; set; }
        public static string downloadPath { get; set; }
    }
}
