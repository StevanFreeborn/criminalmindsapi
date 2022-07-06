using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using server.Models;
using server.Persistence.Repositories;

namespace server.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/characters")]
    [Produces("application/json")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;

        public CharactersController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        /// <summary>
        /// Gets a collection of characters.
        /// </summary>
        /// <param name="filter">Filter parameters passed from query string.</param>
        /// <response code="200">Returns the collection of characters requested.</response>
        /// <response code="400">Not a valid request.</response>
        /// <response code="500">Failed to get characters.</response>
        /// <returns>Returns a collection of characters.</returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Character>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharactersAsync([FromQuery] CharacterFilter? filter)
        {
            try
            {
                var character = await _characterRepository.GetCharactersAsync(filter);
                return Ok(character);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get characters", statusCode: 500);
            }
        }

        /// <summary>
        /// Gets a character by its id.
        /// </summary>
        /// <param name="id">The id of the character being requested.</param>
        /// <response code="200">Returns the character requested.</response>
        /// <response code="400">Not a valid request.</response>
        /// <response code="404">Character with the given id not found.</response>
        /// <response code="500">Failed to get character.</response>
        /// <returns>Returns a character.</returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Character), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharacterByIdAsync(string id)
        {

            if (!ObjectId.TryParse(id, out _))
            {
                ModelState.AddModelError(nameof(id), $"{id} is not a valid id");
                return ValidationProblem();
            }

            try
            {
                var quote = await _characterRepository.GetCharacterByIdAsync(id);

                return quote == null ?
                    Problem(detail: $"Could not find character {id}", statusCode: 404) :
                    Ok(quote);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get character", statusCode: 500);
            }
        }
    }
}
