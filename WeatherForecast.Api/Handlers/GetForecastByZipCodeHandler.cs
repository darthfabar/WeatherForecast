using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Handlers {
    public class GetForecastByZipCodeHandler : IRequestHandler<GetForecastByZipCodeQuery, WeatherForecastResponse> {
        public async Task<WeatherForecastResponse> Handle(GetForecastByZipCodeQuery request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}
