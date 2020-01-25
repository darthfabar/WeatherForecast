using System;

namespace WeatherForecast.Api.Models {
    public class WeatherForecastResponse {
        public string Status { get; set; }
        public string Cityname { get; set; }
        public ForecastForDayResponse[] Forecasts { get; set; }
    }

    public class ForecastForDayResponse {
        public DateTime Date { get; set; }
        public float TemperatureAverageInKelvin { get; set; }
        public float HumidityAverageInPercent { get; set; }
        public float WindSpeedAverageInMeterPerSecond { get; set; }
    }
}
