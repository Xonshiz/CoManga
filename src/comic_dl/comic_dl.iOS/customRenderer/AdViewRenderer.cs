using Google.MobileAds;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(comic_dl.Controls.AdControlView), typeof(comic_dl.iOS.customRenderer.AdViewRenderer))]
namespace comic_dl.iOS.customRenderer
{
    // Google Test Ad Unit = "ca-app-pub-3940256099942544/6300978111";
    public class AdViewRenderer : ViewRenderer
    {
        BannerView adView;
        bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                adView = new BannerView(AdSizeCons.SmartBannerPortrait)
                {
                    AdUnitID = "ca-app-pub-8359860011604747/6645753845",
                    RootViewController = GetRootViewController()
                };

                adView.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen) this.AddSubview(adView);
                    viewOnScreen = true;
                };

                var request = Request.GetDefaultRequest();
                e.NewElement.HeightRequest = GetSmartBannerDpHeight();
                adView.LoadRequest(request);

                base.SetNativeControl(adView);
            }
        }

        private UIViewController GetRootViewController()
        {
            foreach (UIWindow window in UIApplication.SharedApplication.Windows)
            {
                if (window.RootViewController != null)
                {
                    return window.RootViewController;
                }
            }

            return null;
        }

        private int GetSmartBannerDpHeight()
        {
            var dpHeight = (double)UIScreen.MainScreen.Bounds.Height;

            if (dpHeight <= 400) return 32;
            if (dpHeight <= 720) return 50;
            return 90;
        }
    }

}
