using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Queries {
    public class GetForecastByZipCodeQuery : IRequest<WeatherForecastResponse>{
        public string ZipCode { get; }
        public GetForecastByZipCodeQuery(string zipcode) {
            ZipCode = zipcode;
        }
    }
}
