using System;
using System.Collections.Specialized;
using System.Linq;
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
            return await GetWeatherForecastBase(parameter);
        }

        public async Task<OpenweatherForecastResponse> GetWeatherForecastByZipcode(string zipcode, int cnt) {
            var parameter = WeatherForecastParameterBuilder
                .Start()
                .AddZip(zipcode, _openweatherApiSettings.CountryCode)
                .AddNumberOfResults(cnt)
                .AddAppKey(_openweatherApiSettings.AppId);
            return await GetWeatherForecastBase(parameter);
        }

        private async Task<OpenweatherForecastResponse> GetWeatherForecastBase(WeatherForecastParameterBuilder parameter) {
            using (var httpclient = _httpClientFactory.GetHttpClient()) {
                try {
                    httpclient.BaseAddress = new Uri(_openweatherApiSettings.BaseUri);
                    var response = await httpclient.GetAsync("forecast/" + parameter.ToString());

                    OpenweatherForecastResponse result = null;
                    if (response.IsSuccessStatusCode) {
                        result = JsonSerializer.Deserialize<OpenweatherForecastResponse>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {

                    }
                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound) {

                    }
                }
                catch (Exception) {

                    throw;
                }
            }
            throw new NotImplementedException();
        }
    }
}
