using MediatR;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Queries {
    public class GetForecastByCityCodeQuery : IRequest<WeatherForecastResponse> {
        public string City { get; }
        public GetForecastByCityCodeQuery(string city) {
            City = city;
        }
    }
}
