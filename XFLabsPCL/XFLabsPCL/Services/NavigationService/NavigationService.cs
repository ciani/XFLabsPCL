[assembly: Xamarin.Forms.Dependency(typeof(XFLabsPCL.Services.NavigationService))]
namespace XFLabsPCL.Services
{
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XFLabsPCL.Features.Colors;
    using XFLabsPCL.Features.Main;
    using XFLabsPCL.Features.Map;

    public class NavigationService : INavigationService
    {
        private INavigation navigation =>
           (Application.Current.MainPage as NavigationPage).Navigation;

        /// <summary>
        /// Navigates to color list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task NavigateToColorListAsync()
        {
            await navigation.PushAsync(new ColorView());
        }

        /// <summary>
        /// Navigates to m ain view asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task NavigateToMainViewAsync()
        {
            await navigation.PushAsync(new MainView());
        }

        public async Task NavigateToMapAsync()
        {
            await navigation.PushAsync(new MapView());
        }

        /// <summary>
        /// Pops the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task PopAsync()
        {
            await navigation.PopAsync();
        }
    }
    
}
