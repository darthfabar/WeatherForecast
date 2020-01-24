using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecast.Api.ExternalServices.Openweathermap {
    public class WeatherForecastParameterBuilder {
        private Dictionary<string, string> _parameterDict;
        public WeatherForecastParameterBuilder() {
            _parameterDict = new Dictionary<string, string>();
        }
        public static WeatherForecastParameterBuilder Start() {
            return new WeatherForecastParameterBuilder();
        }

        public WeatherForecastParameterBuilder AddSearchword(string search, string countrycode = null) {
            var key = search;
            if (!string.IsNullOrEmpty(countrycode)) key += $",{countrycode}";
            Add("q", key);
            return this;
        }

        public WeatherForecastParameterBuilder AddZip(string zip, string countrycode = null) {
            var key = zip;
            if (!string.IsNullOrEmpty(countrycode)) key += $",{countrycode}";
            Add("zip", key);
            return this;
        }

        public WeatherForecastParameterBuilder AddCity(string city, string countrycode = null) {
            var key = city;
            if (!string.IsNullOrEmpty(countrycode)) key += $",{countrycode}";
            Add("city", key);
            return this;
        }

        public WeatherForecastParameterBuilder AddNumberOfResults(int cnt) {
            Add(nameof(cnt), cnt.ToString());
            return this;
        }

        public WeatherForecastParameterBuilder AddAppKey(string appkey) {
            Add("APPID", appkey);
            return this;
        }

        public override string ToString() {
            var stringbuilder = new StringBuilder();
            stringbuilder.Append("?");
            stringbuilder.AppendJoin('&', _parameterDict.Select(_ => $"{_.Key}={_.Value}"));
            return stringbuilder.ToString();
        }

        public Dictionary<string,string> GetParameterDict() {
            return _parameterDict;
        }

        private void Add(string key, string value) {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
            if (!_parameterDict.TryAdd(key, value)) {
                _parameterDict[key] = value;
            }
        }
    }
}
