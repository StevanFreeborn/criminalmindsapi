using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Persistence.Repositories;

namespace server.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/episodes")]
    [Produces("application/json")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodesController(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        /// <summary>
        /// Gets a collection of episodes.
        /// </summary>
        /// <param name="filter">Filter parameters passed from query string.</param>
        /// <response code="200">Returns the collection of episodes requested.</response>
        /// <response code="400">Not a valid request.</response>
        /// <response code="500">Failed to get episodes.</response>
        /// <returns>Returns a collection of episodes.</returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Episode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEpisodesAsync([FromQuery] EpisodeFilter? filter)
        {            
            try
            {
                var seasons = await _episodeRepository.GetEpisodesAsync(filter);
                return Ok(seasons);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get episodes", statusCode: 500);
            }
        }

        /// <summary>
        /// Gets an episode by its number in the series.
        /// </summary>
        /// <param name="number">The number of the episode in the series.</param>
        /// <response code="200">Returns the episode requested.</response>
        /// <response code="404">Unable to find an episode with the provided number.</response>
        /// <response code="500">Failed to get episode.</response>
        /// <returns>Returns the episode requested.</returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{number:int}")]
        [ProducesResponseType(typeof(Episode), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEpisodeByNumberAsync(int number)
        {
            try
            {
                var episode = await _episodeRepository.GetEpisodeByNumberAsync(number);

                return episode == null ? 
                    Problem(detail: $"Could not find episode {number}", statusCode: 404) : 
                    Ok(episode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get episode", statusCode: 500);
            }
        }
    }
}
