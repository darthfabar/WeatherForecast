using System;

namespace WeatherForecast.Api.Models {
    public class WeatherForecastResponse {
        public string Status { get; set; }
        public string Cityname { get; set; }
        public CurrentForecastResponse CurrentWeather { get; set; }
        public ForecastForDayResponse[] Forecasts { get; set; }
    }

    public class CurrentForecastResponse {
        public DateTime? Date { get; set; }
        public float? TemperatureInKelvin { get; set; }
        public float? HumidityInPercent { get; set; }
        public float? WindSpeedInMeterPerSecond { get; set; }
    }

    public class ForecastForDayResponse {
        public DateTime Date { get; set; }
        public float TemperatureAverageInKelvin { get; set; }
        public float HumidityAverageInPercent { get; set; }
        public float WindSpeedAverageInMeterPerSecond { get; set; }
    }
}
