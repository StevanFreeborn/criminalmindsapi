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

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Episode>), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<List<Episode>>> GetEpisodesAsync([FromQuery] EpisodeFilter? filter)
        {            
            try
            {
                var seasons = await _episodeRepository.GetEpisodesAsync(filter);
                return Ok(seasons);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get episode", statusCode: 500);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{number:int}")]
        [ProducesResponseType(typeof(Episode), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<Episode>> GetEpisodeByNumberAsync(int number)
        {
            try
            {
                var episode = await _episodeRepository.GetEpisodeByNumberAsync(number);

                if (episode == null) return Problem(detail: $"Could not find episode {number}", statusCode: 404);

                return Ok(episode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get episode", statusCode: 500);
            }
        }
    }
}
