[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(XFLabsPCL.Controls.CustomButtonControl)
                                      , typeof(XFLabsPCL.UWP.Renderers.CustomButtonControlRenderer))]

namespace XFLabsPCL.UWP.Renderers
{
    using Windows.UI;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Xamarin.Forms.Platform.UWP;

    public class CustomButtonControlRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = new SolidColorBrush(Colors.Purple);
                (Control as Button).Foreground = new SolidColorBrush(Colors.White);
            }
        }
    }
}
