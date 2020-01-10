using comic_dl.commonFunctionalities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static comic_dl.internalData.CommonInternalData;

namespace comic_dl.Pages.misc
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
            //comicSwitch.IsToggled = false;
            var lul = Preferences.Get(UserSettingkey.HomeResultPreference.ToString(), "false");
            comicSwitch.IsToggled =  Convert.ToBoolean(Preferences.Get(UserSettingkey.HomeResultPreference.ToString(), "false"));
        }

        void Github_Clicked(object sender, System.EventArgs e)
        {
            Launcher.TryOpenAsync(new Uri("https://github.com/Xonshiz/"));
        }

        void ReportIssue_Clicked(object sender, System.EventArgs e)
        {
            Launcher.TryOpenAsync(new Uri("https://github.com/Xonshiz/CoManga/issues/new"));
        }

        void Donate_Clicked(object sender, System.EventArgs e)
        {
            Launcher.TryOpenAsync(new Uri("https://www.paypal.me/Xonshiz"));
        }

        private void ComicSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            mangaSwitch.IsToggled = !e.Value;
            UserAppSettings.SaveUserSetting(UserSettingkey.HomeResultPreference.ToString(), comicSwitch.IsToggled.ToString());
        }

        private void MangaSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            comicSwitch.IsToggled = !e.Value;
            UserAppSettings.SaveUserSetting(UserSettingkey.HomeResultPreference.ToString(), comicSwitch.IsToggled.ToString());
        }
    }
}