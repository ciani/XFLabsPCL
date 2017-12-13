using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;

namespace XFLabsPCL
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Features.Login.LoginView());
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=effe6d95-33c5-42d6-8b0b-b9a8be31faf0;" 
                + "uwp=a69a3dc4-24ff-47bd-8e50-c9559cfe9212;" +
                   "ios={Your iOS App secret here}",
                   typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
