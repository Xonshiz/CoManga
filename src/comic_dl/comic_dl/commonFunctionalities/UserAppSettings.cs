using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace comic_dl.commonFunctionalities
{
    class UserAppSettings
    {
        public static bool SaveUserSetting(string key, string value)
        {
            Preferences.Set(key, value);
            return true;
        }
    }
}
