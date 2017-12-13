namespace XFLabsPCL.Features.Map
{
    using System;
    using System.Threading.Tasks;
    using ReactiveUI;
    using Xamarin.Forms;

    public class MapViewModel : ReactiveObject
    {
        private readonly IGpsService gpsService;
        private ReactiveCommand getGPSPositionCommand;
        public MapViewModel()
        {
            gpsService = DependencyService.Get<IGpsService>();
            getGPSPositionCommand = ReactiveCommand.CreateFromTask(GetPositionAsync);
        }

        public ReactiveCommand GetGPSPositionCommand => getGPSPositionCommand;

        private async Task GetPositionAsync()
        {
            var coordinate = await gpsService.GetCurrentPositionAsync();
        }
    }
}
