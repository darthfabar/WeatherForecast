using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Handlers {
    public class GetForecastByCityHandler : IRequestHandler<GetForecastByCityCodeQuery, WeatherForecastResponse> {
        private readonly Services.IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public GetForecastByCityHandler(Services.IWeatherService weatherService, IMapper mapper) {
            _mapper = mapper;
            _weatherService = weatherService;
        }

        public async Task<WeatherForecastResponse> Handle(GetForecastByCityCodeQuery request, CancellationToken cancellationToken) {
            var weatherdata = await _weatherService.GetWeatherForecastByCity(request.City);
            return CheapMapper(weatherdata);
        }


        private WeatherForecastResponse CheapMapper(Domain.WeatherForecast forecast) {
            if (forecast?.Forecasts is null || forecast.Forecasts.Length == 0) return new WeatherForecastResponse() { Status = "No Results" };

            var groupbyday = forecast.Forecasts.GroupBy(gb => gb.ForecastDatetime.Date);
            var forecastsForResponse = groupbyday.Select(s => new ForecastForDayResponse() {
                Date = s.First().ForecastDatetime.Date,
                HumidityAverageInPercent = s.Sum(s => s.HumidityInPercent ?? 0) / s.Count(),
                TemperatureAverageInKelvin = s.Sum(s => s.TemperatureInKelvin ?? 0) / s.Count(),
                WindSpeedAverageInMeterPerSecond = s.Sum(s => s.WindSpeedInMeterPerSecond ?? 0) / s.Count()
            });

            return new WeatherForecastResponse() {
                Cityname = forecast.City,
                Status = forecast.Status,
                Forecasts = forecastsForResponse.ToArray()
            };
        }
    }
}
