﻿using System;

namespace WeatherForecast.Api.Domain {
    public class WeatherForecast {
        public string City { get; set; }
        public string Status { get; set; }        
        public ForecastDetails[] Forecasts { get; set; }
        public ForecastDetails CurrentWeather { get; set; }
    }

    public class ForecastDetails {
        public DateTime ForecastDatetime { get; set; }
        public float? TemperatureInKelvin { get; set; }        
        public float? HumidityInPercent { get; set; }
        public float? WindSpeedInMeterPerSecond { get; set; }
    }
}
