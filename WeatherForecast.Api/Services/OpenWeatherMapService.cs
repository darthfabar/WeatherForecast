using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Api.ExternalServices.Openweathermap;

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
            var response = await _openWeatherMapClient.GetWeatherForecastByCity(city, NUMBER_OF_RESULTS);
            return _mapper.Map<Domain.WeatherForecast>(response);
        }

        public async Task<Domain.WeatherForecast> GetWeatherForecastByZipcode(string zipcode) {
            var response = await _openWeatherMapClient.GetWeatherForecastByZipcode(zipcode, NUMBER_OF_RESULTS);
            return _mapper.Map<Domain.WeatherForecast>(response);
        }
    }
}
