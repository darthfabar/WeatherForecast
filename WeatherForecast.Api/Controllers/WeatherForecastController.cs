using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator) {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet()]        
        public async Task<IActionResult> GetForecastByZipCode([FromQuery]string zipcode) {
            var query = new GetForecastByZipCodeQuery(zipcode);
            var result = await _mediator.Send(query);
            return result == null ? (IActionResult) NotFound() : Ok(result);           
        }


        [HttpGet()]
        public async Task<IActionResult> GetForecastByCity([FromQuery]string city) {
            var query = new GetForecastByCityCodeQuery(city);
            var result = await _mediator.Send(query);
            return result == null ? (IActionResult)NotFound() : Ok(result);
        }
    }
}
