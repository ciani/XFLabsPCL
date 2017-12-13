[assembly: Xamarin.Forms.Dependency(typeof(XFLabsPCL.iOS.Features.Map.GpsService))]
namespace XFLabsPCL.iOS.Features.Map
{
    using CoreLocation;
    using System.Linq;
    using System.Threading.Tasks;
    using XFLabsPCL.Features.Map;

    public class GpsService : CLLocationManagerDelegate, IGpsService
    {
        private CLLocationManager locationManager;
        private TaskCompletionSource<CoordinateModel> tcs;

        public Task<CoordinateModel> GetCurrentPositionAsync()
        {
            tcs = new TaskCompletionSource<CoordinateModel>();
            locationManager = new CLLocationManager();
            locationManager.Delegate = this;
            locationManager.RequestWhenInUseAuthorization();
            if (CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
            {
                locationManager.RequestLocation();
            }
            else
            {
                tcs.TrySetResult(default(CoordinateModel));
            }

            return tcs.Task;
        }

        public override void UpdatedLocation(CLLocationManager manager, CLLocation newLocation, CLLocation oldLocation) { }

        public override void Failed(CLLocationManager manager, Foundation.NSError error) { }

        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            if (locations != null && locations.Any())
                SetCurrentLocation(locations.First());
        }

        private void SetCurrentLocation(CLLocation currentLocation)
        {
            CoordinateModel currentPosition = new CoordinateModel();

            currentPosition.Latitude = currentLocation.Coordinate.Latitude;
            currentPosition.Longitude = currentLocation.Coordinate.Longitude;
            currentPosition.Altitude = currentLocation.Altitude;

            tcs.TrySetResult(currentPosition);
        }
    }
}