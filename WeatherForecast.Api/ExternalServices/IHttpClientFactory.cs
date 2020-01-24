using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherForecast.Api.ExternalServices {
    public interface IHttpClientFactory {
        HttpClient GetHttpClient();
    }

    public class HttpClientFactory : IHttpClientFactory {
        public HttpClient GetHttpClient() {
            return new HttpClient();
        }
    }
}
