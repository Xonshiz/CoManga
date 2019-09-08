using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Essentials;
using static comic_dl.internalData.CommonInternalData;

namespace comic_dl
{
	public partial class MainPage : Xamarin.Forms.TabbedPage
	{
        public static MainPage Current { get; private set; }

        public MainPage()
		{
			InitializeComponent();
            Current = this;
            if (Convert.ToBoolean(Preferences.Get(UserSettingkey.HomeResultPreference.ToString(), "false")))
            {
                //var _CurrentPage = ;
                CurrentPage = Children.Where(x => x.Title.Contains("Comic")).First();
            }
            
            //var assembly = typeof(EmbeddedImages).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //{
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
            //}
        }

        public static void DisableSwipe()
        {
            Current.On<Android>().DisableSwipePaging();
        }

        public static void EnableSwipe()
        {
            Current.On<Android>().EnableSwipePaging();
        }
    }
}
