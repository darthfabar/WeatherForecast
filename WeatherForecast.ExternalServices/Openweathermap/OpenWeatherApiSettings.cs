namespace WeatherForecast.ExternalServices.Openweathermap {
    public class OpenWeatherApiSettings {
        public string AppId { get; }
        public string BaseUri { get; }
        public string CountryCode { get; }

        public OpenWeatherApiSettings(string baseuri, string appid, string countrycode) {
            AppId = appid;
            BaseUri = baseuri;
            CountryCode = countrycode ?? "de";
        }
    }
}
