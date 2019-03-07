using Android.Gms.Ads;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// Read about implementation here : https://startdebugging.net/how-to-add-admob-to-your-xamarin-forms-app/

[assembly: ExportRenderer(typeof(comic_dl.Controls.MangaDetailAdViewControl), typeof(comic_dl.Droid.customRenderer.MangaAdViewRenderer))]
namespace comic_dl.Droid.customRenderer
{
    // Google Test Ad Unit = "ca-app-pub-3940256099942544/6300978111";
#pragma warning disable CS0618 // Type or member is obsolete
    public class MangaAdViewRenderer : ViewRenderer<Controls.MangaDetailAdViewControl, AdView>
    {
        string adUnitId = "ca-app-pub-8359860011604747/8984707227";
        //Note you may want to adjust this, see further down.
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;
        AdView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;

            // This is a string in the Resources/values/strings.xml that I added or you can modify it here. This comes from admob and contains a / in it
            //adUnitId = Forms.Context.Resources.GetString(Resource.String.banner_ad_unit_id);
            adView = new AdView(Forms.Context);
            adView.AdSize = adSize;
            adView.AdUnitId = adUnitId;

            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest
                            .Builder()
                            .Build());
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.MangaDetailAdViewControl> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}