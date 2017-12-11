namespace XFLabsPCL.Features.Colors
{
    using System;
    using System.Threading.Tasks;
    using ReactiveUI;
    using Refit;
    using XFLabsPCL.Models;
    using System.Linq;
    using Xamarin.Forms;
    using System.Net.Http;

    public class ColorViewModel : ReactiveObject
    {
        private ReactiveCommand getColorsCommand;
        private ObservableAsPropertyHelper<bool> isLoading;
        private ReactiveList<ColorModel> colorList = new ReactiveList<ColorModel>();

        public ColorViewModel()
        {
            getColorsCommand = ReactiveCommand.CreateFromTask(GetColorsAsync);
            getColorsCommand.IsExecuting.ToProperty(this, vm => vm.IsLoading, out isLoading);

        }

        public bool IsLoading => isLoading?.Value ?? false;

        public ReactiveCommand GetColorsCommand => getColorsCommand;

        public ReactiveList<ColorModel> ColorList
        {
            get => colorList;
            set => this.RaiseAndSetIfChanged(ref colorList, value);
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
            var response = await api.GetColorsAsync();
            if (response != null && response.Colors.Any())
                ColorList = new ReactiveList<ColorModel>(response.Colors);

        }
    }
}
