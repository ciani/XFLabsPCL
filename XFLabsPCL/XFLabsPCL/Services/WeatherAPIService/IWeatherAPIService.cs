namespace XFLabsPCL.Services
{
    using Refit;
    using System.Threading.Tasks;
    using XFLabsPCL.Models;

    public interface IWeatherAPIService
    {
        [Get("/weather")]
        Task<WeatherRoot> GetWeatherAsync([AliasAs("q")]string city
            , string units, string appid);
    }
}
