namespace XFLabsPCL.Features.Main
{
    using ReactiveUI;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.IsLoading
                                     , v => v.ActOcupado.IsVisible));

                d(this.Bind(ViewModel, vm => vm.Weather
                                     , v => v.LblWeatherFromAPI.Text));

                d(this.BindCommand(ViewModel, vm => vm.GetWeatherCommand
                                            , v => v.BtnGetWeather));

                d(this.Bind(ViewModel, vm => vm.CurrentPosition.Latitude, v => v.LblLatitude.Text));
                d(this.Bind(ViewModel, vm => vm.CurrentPosition.Longitude, v => v.LblLongitude.Text));
                d(this.BindCommand(ViewModel, vm => vm.GetGPSPositionCommand
                                            , v => v.BtnGetPosition));
            });
        }
    }
}