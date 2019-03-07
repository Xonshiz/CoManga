using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace comic_dl.parsers
{
    class MangaRockMainPageJsonExractor_Parser
    {
        public Task<string> ExtractJsonFromMainPage(string source)
        {
            var x = Regex.Match(source, "window.APP_STATE=(.+)\"code\":0}}");
            
            return Task.FromResult(Convert.ToString(x.Groups[1].Value) + "\"code\":0}}");
        }
    }
}
