using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace comic_dl.cache
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
