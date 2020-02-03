using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Api.Mapper;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Handlers {
    public class GetForecastByZipCodeHandler : IRequestHandler<GetForecastByZipCodeQuery, WeatherForecastResponse> {
        private readonly Services.IWeatherService _weatherService;
        
        public GetForecastByZipCodeHandler(Services.IWeatherService weatherService) {        
            _weatherService = weatherService;
        }
        public async Task<WeatherForecastResponse> Handle(GetForecastByZipCodeQuery request, CancellationToken cancellationToken) {
            var weatherdata = await _weatherService.GetWeatherForecastByZipcode(request.ZipCode);
            return weatherdata.Map();
        }
    }
}
