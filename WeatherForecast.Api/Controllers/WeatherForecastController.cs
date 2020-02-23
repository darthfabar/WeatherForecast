using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Queries;

namespace WeatherForecast.Api.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase {

        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet()]
        [ResponseCache(Duration = 600)]
        public async Task<IActionResult> GetForecastByZipCode([FromQuery]string zipcode) {
            var query = new GetForecastByZipCodeQuery(zipcode);
            var result = await _mediator.Send(query);
            return result == null ? (IActionResult)NotFound() : Ok(result);
        }


        [HttpGet()]
        [ResponseCache(Duration = 600)]
        public async Task<IActionResult> GetForecastByCity([FromQuery]string city) {
            var query = new GetForecastByCityCodeQuery(city);
            var result = await _mediator.Send(query);
            return result == null ? (IActionResult)NotFound() : Ok(result);
        }
    }
}
