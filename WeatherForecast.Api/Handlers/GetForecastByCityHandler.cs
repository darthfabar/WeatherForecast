using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Handlers {
    public class GetForecastByCityHandler : IRequestHandler<GetForecastByCityCodeQuery, WeatherForecastResponse> {
        public async Task<WeatherForecastResponse> Handle(GetForecastByCityCodeQuery request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}
