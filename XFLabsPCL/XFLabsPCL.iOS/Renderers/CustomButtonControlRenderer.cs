[assembly: Xamarin.Forms.ExportRenderer(typeof(XFLabsPCL.Controls.CustomButtonControl)
                                      , typeof(XFLabsPCL.iOS.Renderers.CustomButtonControlRenderer))]
namespace XFLabsPCL.iOS.Renderers
{
    using UIKit;
    using Xamarin.Forms.Platform.iOS;
    public class CustomButtonControlRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.Purple;
                Control.SetTitleColor(UIColor.Red, UIControlState.Disabled);
                Control.SetTitleColor(UIColor.White, UIControlState.Normal);
            }
        }
    }
}