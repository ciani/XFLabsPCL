[assembly: Xamarin.Forms.ExportRenderer(typeof(XFLabsPCL.Controls.CustomButtonControl)
                                      , typeof(XFLabsPCL.Droid.Renderers.CustomButtonControlRenderer))]
namespace XFLabsPCL.Droid.Renderers
{
    using Xamarin.Forms.Platform.Android;
    public class CustomButtonControlRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Purple);
                Control.SetAllCaps(false);
            }
        }
    }
}