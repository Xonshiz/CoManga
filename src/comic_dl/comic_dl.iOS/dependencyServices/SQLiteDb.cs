using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using comic_dl.cache;
using comic_dl.iOS.dependencyServices;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace comic_dl.iOS.dependencyServices
{
    class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "comic_dl_cache.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}