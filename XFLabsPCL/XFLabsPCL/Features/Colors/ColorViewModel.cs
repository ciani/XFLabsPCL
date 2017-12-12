namespace XFLabsPCL.Features.Colors
{
    using System;
    using ReactiveUI;
    using Refit;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XFLabsPCL.Models;
    using XFLabsPCL.Services;
    using System.Windows.Input;

    public class ColorViewModel : ReactiveObject
    {
        private readonly IStorageService storageService;
        private readonly INetworkConnectionCheckerService networkService;
        private ReactiveCommand getColorsCommand;
        private ObservableAsPropertyHelper<bool> isLoading;
        private ReactiveList<ColorModel> colorList = new ReactiveList<ColorModel>();
        private ColorModel selectedColor;

        public ColorViewModel()
        {
            storageService = DependencyService.Get<IStorageService>();
            networkService = DependencyService.Get<INetworkConnectionCheckerService>();
            networkService.Check();
            this.WhenAnyValue(v => v.networkService.IsConnected).Subscribe(async isConnected => 
            {
                if (isConnected && !IsLoading)
                    await GetColorsAsync();
            });
            getColorsCommand = ReactiveCommand.CreateFromTask(GetColorsAsync);
            getColorsCommand.IsExecuting.ToProperty(this, vm => vm.IsLoading, out isLoading);
        }

        public ColorModel SelectedColor
        {
            get { return selectedColor; }
            set { this.RaiseAndSetIfChanged(ref selectedColor, value); }
        }

        public bool IsLoading => isLoading?.Value ?? false;

        public ReactiveCommand GetColorsCommand => getColorsCommand;

        public ReactiveList<ColorModel> ColorList
        {
            get { return colorList; }
            set { this.RaiseAndSetIfChanged(ref colorList, value); }
        }

        private async Task GetColorsAsync()
        {
            HttpMessageHandler nativeHandler = new HttpClientHandler();
            var nativeHandlerService = DependencyService.Get<Services.INativeHttpHandlerService>();
            if (nativeHandlerService != null)
                nativeHandler = nativeHandlerService.GetNativeHandler();

            RefitSettings settings = new RefitSettings();
            settings.HttpMessageHandlerFactory = () =>
            {
                return nativeHandler;
            };
            var api = RestService.For<IColorWebService>("http://reqres.in", settings);
            var response = await storageService.GetOrFetchObjectAsync("colors", async () =>
            {
                try
                {
                    var itemsResponse = await api.GetColorsAsync();
                    return itemsResponse;
                }
                catch (Exception)
                {
                    return null;
                }
            });

            if (response != null && response.Colors != null)
                ColorList = new ReactiveList<ColorModel>(response.Colors);
        }
    }
}
