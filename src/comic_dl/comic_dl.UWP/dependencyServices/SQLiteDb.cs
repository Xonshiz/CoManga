using comic_dl.cache;
using comic_dl.UWP.dependencyServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace comic_dl.UWP.dependencyServices
{
    class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(documentsPath, "comic_dl_cache.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}
