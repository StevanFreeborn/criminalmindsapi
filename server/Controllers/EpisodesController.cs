using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Persistence.Repositories;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/episodes")]
    [Produces("application/json")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodesController(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Episode>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Episode>>> GetEpisodesAsync([FromQuery]EpisodeFilter? filter)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var seasons = await _episodeRepository.GetEpisodesAsync(filter);
#pragma warning restore CS8604 // Possible null reference argument.
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
