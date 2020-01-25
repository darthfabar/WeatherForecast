using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WeatherForecast.Api.Models;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Handlers {
    public class GetForecastByCityHandler : IRequestHandler<GetForecastByCityCodeQuery, WeatherForecastResponse> {
        private readonly Services.IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public GetForecastByCityHandler(Services.IWeatherService weatherService, IMapper mapper) {
            _mapper = mapper;
            _weatherService = weatherService;
        }

        public async Task<WeatherForecastResponse> Handle(GetForecastByCityCodeQuery request, CancellationToken cancellationToken) {
            var weatherdata = await _weatherService.GetWeatherForecastByCity(request.City);
            return _mapper.Map<WeatherForecastResponse>(weatherdata);
        }
    }
}
