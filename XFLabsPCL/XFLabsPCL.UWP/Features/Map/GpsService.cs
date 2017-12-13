[assembly: Xamarin.Forms.Dependency(typeof(XFLabsPCL.UWP.Features.Map.GpsService))]
namespace XFLabsPCL.UWP.Features.Map
{
    using System;
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;
    using XFLabsPCL.Features.Map;

    public class GpsService : IGpsService
    {
        public async Task<CoordinateModel> GetCurrentPositionAsync()
        {
            CoordinateModel currentPosition = new CoordinateModel();

            var accessResult = await Geolocator.RequestAccessAsync();
            if (accessResult == GeolocationAccessStatus.Allowed)
            {
                Geolocator geolocator = new Geolocator() { DesiredAccuracy = PositionAccuracy.Default };
                Geoposition geoPosition = await geolocator.GetGeopositionAsync();
                if (geoPosition?.Coordinate?.Point != null)
                {
                    currentPosition.Latitude = geoPosition.Coordinate.Point.Position.Latitude;
                    currentPosition.Longitude = geoPosition.Coordinate.Point.Position.Longitude;
                    currentPosition.Altitude = geoPosition.Coordinate.Point.Position.Altitude;
                    if (geoPosition.Coordinate.Heading.HasValue)
                        currentPosition.Heading = geoPosition.Coordinate.Heading.Value;
                }
            }

            return currentPosition;
        }
    }
}
