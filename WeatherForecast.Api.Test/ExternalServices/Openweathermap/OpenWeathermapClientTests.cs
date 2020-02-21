using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Moq;
using WeatherForecast.ExternalServices.Openweathermap;

namespace WeatherForecast.Api.ExternalServices.Openweathermap.Tests {
    [TestClass()]
    public class OpenWeathermapClientTests {

        [TestMethod()]
        public async Task GetWeatherForecastTest() {
            //TODO wegmocken
            var httpMessageHandlerMock = new Mock<FakeHttpMessageHandler>() { CallBase = true };
            httpMessageHandlerMock.Setup(s => s.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage() {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(Test.Properties.Resources.sampleResponse)
                });


            var httpclientfactory = new HttpClient(httpMessageHandlerMock.Object);
            var apisettings = new OpenWeatherApiSettings("http://api.openweathermap.org/data/2.5/", "TESTAPPKEY", null);
            var forecastclient = new OpenWeathermapClient(apisettings, httpclientfactory);

            var result = await forecastclient.GetWeatherForecastByCity("Leipzig", 50);

            Assert.IsNotNull(result);
        }
    }

    public class FakeHttpMessageHandler : HttpMessageHandler {
        public virtual HttpResponseMessage Send(HttpRequestMessage request) {
            throw new NotImplementedException("Now we can setup this method with our mocking framework");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {
            return Task.FromResult(Send(request));
        }
    }
}