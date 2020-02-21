using System.Threading.Tasks;

namespace WeatherForecast.ExternalServices.Openweathermap {
    public interface IOpenWeathermapClient {
        Task<OpenweatherForecastResponse> GetWeatherForecastByZipcode(string zipcode, int cnt);
        Task<OpenweatherForecastResponse> GetWeatherForecastByCity(string city, int cnt);
        Task<OpenweatherCurrentForecastResponse> GetCurrentWeatherForecast(string q);
    }
}
