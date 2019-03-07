/*
 * @Author : Xonshiz (Dhruv Kanojia)
 * @Email : xonshiz@gmail.com
 * @Paypal : https://www.paypal.me/Xonshiz
 */
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace comic_dl
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart ()
		{
            // Handle when your app starts
            AppCenter.Start("android=a395c403-61c1-4ead-ab19-41fdeb9a383c;" + "uwp=6176a0c5-bfe2-4b2b-9db8-3e837206fe8f;" + "ios=1c18cb41-8fd3-4d77-bbb7-5d8413bb80ce;", typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
