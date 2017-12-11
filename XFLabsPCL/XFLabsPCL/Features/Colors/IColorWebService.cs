namespace XFLabsPCL.Features.Colors
{
    using Refit;
    using System.Threading.Tasks;
    using XFLabsPCL.Models;

    public interface IColorWebService
    {
        [Get("/api/unknown")]
        Task<ResponseModel> GetColorsAsync(int per_page = 10, int delay = 5);
    }
}
