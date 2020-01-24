namespace WeatherForecast.Api.ExternalServices.Openweathermap {
    public class OpenWeatherApiSettings {
        public string AppId { get; set; }
        public string BaseUri { get; set; }
        public string CountryCode { get; set; } = "de";
    }
}
