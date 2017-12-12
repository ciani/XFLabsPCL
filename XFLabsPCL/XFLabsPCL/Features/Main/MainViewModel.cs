namespace XFLabsPCL.Features.Main
{
    using ReactiveUI;
    using Refit;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XFLabsPCL.Services;

    public class MainViewModel : ReactiveObject
    {
        private string weather;
        private ReactiveCommand getWeatherCommand;
        private ObservableAsPropertyHelper<bool> isLoading;

        public MainViewModel()
        {
            getWeatherCommand = ReactiveCommand.CreateFromTask(GetWeatherAsync);
            getWeatherCommand.IsExecuting.ToProperty(this, vm => vm.IsLoading, out isLoading);
        }

        public bool IsLoading => isLoading?.Value ?? false;

        public string Weather
        {
            get { return weather; }
            set { this.RaiseAndSetIfChanged(ref weather, value); }
        }

        public ReactiveCommand GetWeatherCommand => getWeatherCommand;

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

    }
}
