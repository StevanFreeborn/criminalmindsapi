using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Persistence.Repositories;

namespace server.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/seasons")]
    [Produces("application/json")]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonsController(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Season>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<List<Season>>> GetSeasonsAsync()
        {
            try
            {
                var seasons = await _seasonRepository.GetSeasonsAsync();
                return Ok(seasons);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get season", statusCode: 500);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{number:int}")]
        [ProducesResponseType(typeof(Season), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails),500)]
        public async Task<ActionResult<Season>> GetSeasonByNumberAsync(int number)
        {
            try
            {
                var season = await _seasonRepository.GetSeasonByNumberAsync(number);

                if (season == null) return Problem(detail: $"Could not find season {number}", statusCode: 404);

                return Ok(season);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get season", statusCode: 500);
            }
        }
    }
}
