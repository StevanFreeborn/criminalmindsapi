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

        /// <summary>
        /// Gets a collection of seasons.
        /// </summary>
        /// <response code="200">Returns the collection of seasons requested.</response>
        /// <response code="500">Failed to get seasons.</response>
        /// <returns>Returns a collection of seasons.</returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Season>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Gets a season by its number.
        /// </summary>
        /// <response code="200">Returns the season requested.</response>
        /// <response code="404">Could not find a seaon with number provided.</response>
        /// <response code="500">Failed to get season.</response>
        /// <returns>Returns the season requested</returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{number:int}")]
        [ProducesResponseType(typeof(Season), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Season>> GetSeasonByNumberAsync(int number)
        {
            try
            {
                var season = await _seasonRepository.GetSeasonByNumberAsync(number);

                return season == null ? 
                    Problem(detail: $"Could not find season {number}", statusCode: 404) : 
                    Ok(season);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get season", statusCode: 500);
            }
        }
    }
}
