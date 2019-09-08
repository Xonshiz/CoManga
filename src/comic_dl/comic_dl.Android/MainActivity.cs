using System;
using Xamarin.Forms;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PanCardView.Droid;
using ImageCircle.Forms.Plugin.Droid;
using Android.Gms.Ads;
using Plugin.Permissions;

namespace comic_dl.Droid
{
    [Activity(Label = "CoManga", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code, it may also be called: bundle

            global::Xamarin.Forms.Forms.Init(this, bundle);

            FormsMaterial.Init(this, bundle);
            CardsViewRenderer.Preserve();
            ImageCircleRenderer.Init();
            
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-8359860011604747~6496691983");
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

