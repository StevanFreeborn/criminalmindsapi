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
        [ProducesResponseType(500)]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get episodes");
            }
        }
    }
}
