using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace comic_dl.Pages.misc
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
        }

        void Github_Clicked(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://github.com/Xonshiz/"));
        }

        void ReportIssue_Clicked(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://github.com/Xonshiz/comic-dl-cross-platform-app/issues/new"));
        }

        void Donate_Clicked(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.paypal.me/Xonshiz"));
        }
    }
}