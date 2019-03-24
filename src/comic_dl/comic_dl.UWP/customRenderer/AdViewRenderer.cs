using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Advertising.Ads;
using Microsoft.Advertising.WinRT.UI;
using Windows.System.Profile;

[assembly: ExportRenderer(typeof(comic_dl.Controls.AdControlView), typeof(comic_dl.UWP.customRenderer.AdViewRenderer))]
namespace comic_dl.UWP.customRenderer
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class AdViewRenderer : ViewRenderer<Controls.AdControlView, AdControl>
    {
        string bannerId = "1100041955";
        AdControl adView;
        string applicationID = "9n81f8b5ww93";
        void CreateNativeAdControl()
        {
            if (adView != null)
                return;

            var width = 300;
            var height = 50;
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop")
            {
                width = 728;
                height = 90;
            }
            // Setup your BannerView, review AdSizeCons class for more Ad sizes. 
            adView = new AdControl
            {
                ApplicationId = applicationID,
                AdUnitId = bannerId,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom,
                Height = height,
                Width = width
            };

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.AdControlView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                //SetNativeControl(adView);
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
