using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WeatherForecast.ExternalServices.Openweathermap;

namespace WeatherForecast.Api.Mapper.Tests {
    [TestClass()]
    public class DatetimeValueConverterTests {
        [TestMethod()]
        public void ConvertTest() {
            var converter = new DatetimeValueResolver();

            var list = new List() {
                dt_txt = "2020-01-25 12:00:00",
                dt = 1579953600
            };

            var date = converter.Resolve(list, new Domain.ForecastDetails(), new DateTime(), null);

            Assert.AreEqual(DateTimeKind.Utc, date.Kind);
            Assert.AreEqual(2020, date.Year);
            Assert.AreEqual(01, date.Month);
            Assert.AreEqual(25, date.Day);
            Assert.AreEqual(12, date.Hour);
            Assert.AreEqual(00, date.Minute);
            Assert.AreEqual(00, date.Second);
        }
    }
}