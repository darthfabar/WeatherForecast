using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.ExternalServices.Openweathermap;

namespace WeatherForecast.Api.Services {
    public class OpenWeatherMapService : IWeatherService {
        private readonly IOpenWeathermapClient _openWeatherMapClient;
        private readonly IMapper _mapper;
        private const int NUMBER_OF_RESULTS = 50;
        public OpenWeatherMapService(IMapper mapper, IOpenWeathermapClient openWeathermapClient) {
            _mapper = mapper;
            _openWeatherMapClient = openWeathermapClient;
        }
        public async Task<Domain.WeatherForecast> GetWeatherForecastByCity(string city) {
            return await GetAndMapWeatherdata(_openWeatherMapClient.GetWeatherForecastByCity(city, NUMBER_OF_RESULTS), city);
        }

        public async Task<Domain.WeatherForecast> GetWeatherForecastByZipcode(string zipcode) {
            return await GetAndMapWeatherdata(_openWeatherMapClient.GetWeatherForecastByZipcode(zipcode, NUMBER_OF_RESULTS), zipcode);
        }

        private async Task<Domain.WeatherForecast> GetAndMapWeatherdata(Task<OpenweatherForecastResponse> forecastsTask, string query) {
            var forecastsResponse = await forecastsTask;
            var currenweatherresponse = await _openWeatherMapClient.GetCurrentWeatherForecast(query);
          
            var result = _mapper.Map<Domain.WeatherForecast>(forecastsResponse);
            var currentForecast = _mapper.Map<Domain.ForecastDetails>(currenweatherresponse);
            if(result != null) result.CurrentWeather = currentForecast;            
            return result;
        }
    }
}
