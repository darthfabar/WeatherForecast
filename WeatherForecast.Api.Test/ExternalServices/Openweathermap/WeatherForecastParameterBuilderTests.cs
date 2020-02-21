using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WeatherForecast.ExternalServices.Openweathermap;

namespace WeatherForecast.Api.ExternalServices.Openweathermap.Tests {
    [TestClass()]
    public class WeatherForecastParameterBuilderTests {

        [TestMethod()]
        public void StartTest() {
            var parameterbuilder = WeatherForecastParameterBuilder.Start();
            Assert.IsNotNull(parameterbuilder);
        }

        [TestMethod()]
        public void AddSearchwordTest() {
            var searchword = "Leipzig";
            var parameterbuilder = WeatherForecastParameterBuilder
                .Start()
                .AddCity(searchword);

            Assert.AreEqual(1, parameterbuilder.GetParameterDict().Count);
            Assert.AreEqual(searchword, parameterbuilder.GetParameterDict()["q"]);
        }
        
        [TestMethod()]
        public void AddSearchwordWithCountryCodeTest() {
            var searchword = "Leipzig";
            var parameterbuilder = WeatherForecastParameterBuilder
                .Start()
                .AddCity(searchword,"de");

            Assert.AreEqual(1, parameterbuilder.GetParameterDict().Count);
            Assert.AreEqual(searchword + ",de", parameterbuilder.GetParameterDict()["q"]);
        }

        [TestMethod()]
        public void AddParameterTwoTimes() {
            var searchword = "Leipzig";
            var searchword2 = "Erfurt";
            var parameterbuilder = WeatherForecastParameterBuilder
                .Start()
                .AddCity(searchword)             
                .AddCity(searchword2);                

            Assert.AreEqual(1, parameterbuilder.GetParameterDict().Count);
            Assert.AreEqual(searchword2, parameterbuilder.GetParameterDict()["q"]);
        }

        [TestMethod()]
        public void AddNumberOfResultsTest() {
            var numberofresults = 50;
            var parameterbuilder = WeatherForecastParameterBuilder
                .Start()
                .AddNumberOfResults(numberofresults);

            Assert.AreEqual(1, parameterbuilder.GetParameterDict().Count);
            Assert.AreEqual(numberofresults.ToString(), parameterbuilder.GetParameterDict()["cnt"]);
        }

        [TestMethod()]
        public void AddAppKeyTest() {
            var appkey = Guid.NewGuid().ToString();
            var parameterbuilder = WeatherForecastParameterBuilder
                .Start()
                .AddAppKey(appkey);

            Assert.AreEqual(1, parameterbuilder.GetParameterDict().Count);
            Assert.AreEqual(appkey, parameterbuilder.GetParameterDict()["APPID"]);
        }

        [TestMethod()]
        public void ToStringTest() {
            var searchword = "Leipzig";
            var appkey = Guid.NewGuid().ToString();
            var numberOfResults = 50;

            var parameterbuilder = WeatherForecastParameterBuilder
                .Start()
                .AddCity(searchword)
                .AddNumberOfResults(numberOfResults)
                .AddAppKey(appkey);

            var urlParameter = parameterbuilder.ToString();
            Assert.AreEqual(3, parameterbuilder.GetParameterDict().Count);
            Assert.AreEqual($"?q={searchword}&cnt={numberOfResults}&APPID={appkey}", urlParameter);            
        }
    }
}