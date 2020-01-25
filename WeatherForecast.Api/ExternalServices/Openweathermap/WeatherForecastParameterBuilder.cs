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

        public WeatherForecastParameterBuilder AddZip(string zip, string countrycode = null) {
            AddWithCountyCode("zip", zip, countrycode);            
            return this;
        }

        public WeatherForecastParameterBuilder AddCity(string city, string countrycode = null) {
            AddWithCountyCode("q", city, countrycode);            
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

        private void AddWithCountyCode(string key, string value, string countrycode = null) {            
            if (!string.IsNullOrEmpty(countrycode)) value += $",{countrycode}";
            Add(key, value);            
        }

        private void Add(string key, string value) {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
            if (!_parameterDict.TryAdd(key, value)) {
                _parameterDict[key] = value;
            }
        }
    }
}
