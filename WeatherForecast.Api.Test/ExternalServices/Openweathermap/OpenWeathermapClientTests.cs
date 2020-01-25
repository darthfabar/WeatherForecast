using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherForecast.Api.ExternalServices.Openweathermap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Api.ExternalServices.Openweathermap.Tests {
    [TestClass()]
    public class OpenWeathermapClientTests {
        [TestMethod()]
        public void OpenWeathermapClientTest() {
            Assert.Fail();
        }

        //TODO falscher APPkey abfangen

        //TODO keinResult abfangen

        //401, 404, 500

        [TestMethod()]
        public async Task GetWeatherForecastTest() {
            //TODO wegmocken
            var httpclientfactory = new HttpClientFactory();
            var apisettings = new OpenWeatherApiSettings("http://api.openweathermap.org/data/2.5/", "TESTAPPKEY", null);
            var forecastclient = new OpenWeathermapClient(apisettings, httpclientfactory);

            var result = await forecastclient.GetWeatherForecastByCity("Leipzig", 50);

            Assert.IsNotNull(result);
        }
    }
}