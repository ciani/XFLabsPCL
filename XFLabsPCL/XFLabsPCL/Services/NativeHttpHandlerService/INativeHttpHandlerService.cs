namespace XFLabsPCL.Services
{
    using System.Net.Http;
    public interface INativeHttpHandlerService
    {
        HttpMessageHandler GetNativeHandler();
    }
}
