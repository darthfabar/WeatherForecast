using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherForecast.Api.Mapper;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Handlers {
    public class GetForecastByCityHandler : IRequestHandler<GetForecastByCityCodeQuery, WeatherForecastResponse> {
        private readonly Services.IWeatherService _weatherService;

        public GetForecastByCityHandler(Services.IWeatherService weatherService) {
            _weatherService = weatherService;
        }

        public async Task<WeatherForecastResponse> Handle(GetForecastByCityCodeQuery request, CancellationToken cancellationToken) {
            var weatherdata = await _weatherService.GetWeatherForecastByCity(request.City);
            return weatherdata.Map();            
        }
    }
}
