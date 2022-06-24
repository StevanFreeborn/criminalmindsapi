using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using server.Models;
using server.Persistence.Repositories;

namespace server.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/quotes")]
    [Produces("application/json")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuotesController(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        /// <summary>
        /// Gets a collection of quotes.
        /// </summary>
        /// <param name="filter">Filter parameters passed from query string.</param>
        /// <response code="200">Returns the collection of quotes requested.</response>
        /// <response code="400">Not a valid request.</response>
        /// <response code="500">Failed to get quotes.</response>
        /// <returns>Returns a collection of quotes.</returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Quote>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Quote>>> GetQuotesAsync([FromQuery] QuoteFilter? filter)
        {
            try
            {
                var quotes = await _quoteRepository.GetQuotesAsync(filter);
                return Ok(quotes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get quotes", statusCode: 500);
            }
        }

        // TODO: Add xml comments
        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Quote), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Quote>> GetQuoteByIdAsync(string id)
        {

            if (!ObjectId.TryParse(id, out _))
            {
                ModelState.AddModelError(nameof(id), $"{id} is not a valid id");
                return ValidationProblem();
            }

            try
            {
                var quote = await _quoteRepository.GetQuoteByIdAsync(id);

                return quote == null ?
                    Problem(detail: $"Could not find quote {id}", statusCode: 404) :
                    Ok(quote);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(detail: "Failed to get quote", statusCode: 500);
            }
        }
    }
}
