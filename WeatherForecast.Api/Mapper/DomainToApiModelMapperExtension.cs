using System.Linq;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Mapper {
    public static class DomainToApiModelMapperExtension {

        public static WeatherForecastResponse Map(this Domain.WeatherForecast forecast) {
            if (forecast?.Forecasts is null || forecast.Forecasts.Length == 0) return new WeatherForecastResponse() { Status = "No Results", Cityname = forecast?.City };

            var groupbyday = forecast.Forecasts.GroupBy(gb => gb.ForecastDatetime.Date);
            var forecastsForResponse = groupbyday.Select(s => new ForecastForDayResponse() {
                Date = s.First().ForecastDatetime.Date,
                HumidityAverageInPercent = s.Sum(s => s.HumidityInPercent ?? 0) / s.Count(w => w.HumidityInPercent != null),
                TemperatureAverageInKelvin = s.Sum(s => s.TemperatureInKelvin ?? 0) / s.Count(w => w.TemperatureInKelvin != null),
                WindSpeedAverageInMeterPerSecond = s.Sum(s => s.WindSpeedInMeterPerSecond ?? 0) / s.Count(w => w.WindSpeedInMeterPerSecond != null)
            });

            return new WeatherForecastResponse() {
                Cityname = forecast.City,
                Status = forecast.Status,
                Forecasts = forecastsForResponse.ToArray(),
                CurrentWeather = forecast.MapCurrentForecast()
            };
        }

        private static CurrentForecastResponse MapCurrentForecast(this Domain.WeatherForecast forecast) {
            if (forecast?.CurrentWeather is null) return null;

            return new CurrentForecastResponse {
                Date = forecast.CurrentWeather.ForecastDatetime,
                HumidityInPercent = forecast.CurrentWeather.HumidityInPercent,
                TemperatureInKelvin = forecast.CurrentWeather.TemperatureInKelvin,
                WindSpeedInMeterPerSecond = forecast.CurrentWeather.WindSpeedInMeterPerSecond,
            };

        }

    }
}
