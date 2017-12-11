[assembly: Xamarin.Forms.Dependency(typeof(XFLabsPCL.Droid.Services.NativeHttpHandlerService))]
namespace XFLabsPCL.Droid.Services
{
    using System.Net.Http;
    using Xamarin.Android.Net;
    using XFLabsPCL.Services;

    public class NativeHttpHandlerService : INativeHttpHandlerService
    {
        public HttpMessageHandler GetNativeHandler()
        {
            AndroidClientHandler handler = new AndroidClientHandler();
            return handler;
        }
    }
}