using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Api.ExternalServices.Openweathermap {
    public interface IOpenWeathermapClient {
        Task<OpenweatherForecastResponse> GetWeatherForecastByZipcode(string zipcode, int cnt);
        Task<OpenweatherForecastResponse> GetWeatherForecastByCity(string city, int cnt);
    }
}
