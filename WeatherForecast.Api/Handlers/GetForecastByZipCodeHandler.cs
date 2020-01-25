using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Handlers {
    public class GetForecastByZipCodeHandler : IRequestHandler<GetForecastByZipCodeQuery, WeatherForecastResponse> {
        private readonly Services.IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public GetForecastByZipCodeHandler(Services.IWeatherService weatherService, IMapper mapper) {
            _mapper = mapper;
            _weatherService = weatherService;
        }
        public async Task<WeatherForecastResponse> Handle(GetForecastByZipCodeQuery request, CancellationToken cancellationToken) {
            var weatherdata = await _weatherService.GetWeatherForecastByZipcode(request.ZipCode);
            return _mapper.Map<WeatherForecastResponse>(weatherdata);
        }
    }
}
