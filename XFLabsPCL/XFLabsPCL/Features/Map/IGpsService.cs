namespace XFLabsPCL.Features.Map
{
    using System.Threading.Tasks;

    public interface IGpsService
    {
        Task<CoordinateModel> GetCurrentPositionAsync();
    }
}
