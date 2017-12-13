namespace XFLabsPCL.Features.Main
{
    using ReactiveUI;
    using Refit;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XFLabsPCL.Features.Map;
    using XFLabsPCL.Services;

    public class MainViewModel : ReactiveObject
    {
        private readonly IGpsService gpsService;
        private string weather;
        private CoordinateModel currentPosition;
        private ReactiveCommand getWeatherCommand;
        private ReactiveCommand getGPSPositionCommand;
        private ObservableAsPropertyHelper<bool> isLoading;

        public MainViewModel()
        {
            gpsService = DependencyService.Get<IGpsService>();
            getGPSPositionCommand = ReactiveCommand.CreateFromTask(GetPositionAsync);
            getWeatherCommand = ReactiveCommand.CreateFromTask(GetWeatherAsync);
            getWeatherCommand.IsExecuting.ToProperty(this, vm => vm.IsLoading, out isLoading);
        }

        public CoordinateModel CurrentPosition
        {
            get { return currentPosition; }
            set { this.RaiseAndSetIfChanged(ref currentPosition, value); }
        }

        public bool IsLoading => isLoading?.Value ?? false;

        public string Weather
        {
            get { return weather; }
            set { this.RaiseAndSetIfChanged(ref weather, value); }
        }

        public ReactiveCommand GetWeatherCommand => getWeatherCommand;
        public ReactiveCommand GetGPSPositionCommand => getGPSPositionCommand;

        private async Task GetWeatherAsync()
        {
            var nativeHandlerService = DependencyService.Get<INativeHttpHandlerService>();
            var nativeHandler = nativeHandlerService.GetNativeHandler();

            RefitSettings settings = new RefitSettings();
            settings.HttpMessageHandlerFactory = () =>
            {
                return nativeHandler;
            };

            var api = RestService.For<Services.IWeatherAPIService>("http://api.openweathermap.org/data/2.5");
            var weather = await api.GetWeatherAsync("Madrid", "Metric", "fc9f6c524fc093759cd28d41fda89a1b");
            Weather = weather.DisplayTemp;
        }

        private async Task GetPositionAsync()
        {
            CurrentPosition = await gpsService.GetCurrentPositionAsync();
        }

    }
}
