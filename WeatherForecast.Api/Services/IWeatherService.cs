using System.Threading.Tasks;
namespace WeatherForecast.Api.Services {
    /// <summary>
    /// Have an Abstractionlayer to be able to support different Weatherprovider
    /// </summary>
    public interface IWeatherService {
        Task<Domain.WeatherForecast> GetWeatherForecastByCity(string city);
        Task<Domain.WeatherForecast> GetWeatherForecastByZipcode(string zipcode);
    }
}
