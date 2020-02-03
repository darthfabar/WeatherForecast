using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherForecast.Api.ExternalServices.Openweathermap {
    public class OpenWeathermapClient : IOpenWeathermapClient {
        private readonly OpenWeatherApiSettings _openweatherApiSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public OpenWeathermapClient(OpenWeatherApiSettings openweatherApiSettings, IHttpClientFactory httpClientFactory) {
            _openweatherApiSettings = openweatherApiSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OpenweatherForecastResponse> GetWeatherForecastByCity(string city, int cnt) {
            var parameter = WeatherForecastParameterBuilder
                .Start()
                .AddCity(city, _openweatherApiSettings.CountryCode)
                .AddNumberOfResults(cnt)
                .AddAppKey(_openweatherApiSettings.AppId);
            return await GetWeatherForecastsBase(parameter);
        }

        public async Task<OpenweatherCurrentForecastResponse> GetCurrentWeatherForecast(string q) {
            var parameter = WeatherForecastParameterBuilder
                .Start()
                .AddCity(q, _openweatherApiSettings.CountryCode)
                .AddAppKey(_openweatherApiSettings.AppId);

            var httpResponse = await GetOpenWeaterDataBase(parameter, "weather");

            var result = JsonSerializer.Deserialize<OpenweatherCurrentForecastResponse>(await httpResponse.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<OpenweatherForecastResponse> GetWeatherForecastByZipcode(string zipcode, int cnt) {
            var parameter = WeatherForecastParameterBuilder
                .Start()
                .AddZip(zipcode, _openweatherApiSettings.CountryCode)
                .AddNumberOfResults(cnt)
                .AddAppKey(_openweatherApiSettings.AppId);
            return await GetWeatherForecastsBase(parameter);
        }

        private async Task<OpenweatherForecastResponse> GetWeatherForecastsBase(WeatherForecastParameterBuilder parameter, string route = "forecast/") {
            var httpResponse = await GetOpenWeaterDataBase(parameter, route);
            if (httpResponse == null) return null;

            var result = JsonSerializer.Deserialize<OpenweatherForecastResponse>(await httpResponse.Content.ReadAsStringAsync());
            return result;            
        }
        private async Task<HttpResponseMessage> GetOpenWeaterDataBase(WeatherForecastParameterBuilder parameter, string route) {
            using (var httpclient = _httpClientFactory.GetHttpClient()) {
                try {
                    httpclient.BaseAddress = new Uri(_openweatherApiSettings.BaseUri);
                    var response = await httpclient.GetAsync(route + parameter.ToString());                    
                    if (response.IsSuccessStatusCode) {                        
                        return response;
                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                        throw new Exception("Unauthorized");
                    }
                    return null;
                }
                catch (Exception) {
                    throw;
                }
            }
        }
    }
}
