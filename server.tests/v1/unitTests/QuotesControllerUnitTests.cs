using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using System.Net;

namespace server.tests.v1.UnitTests
{
    public class QuotesControllerUnitTests
    {
        private readonly Mock<IQuoteRepository> _mockRepo;
        private readonly QuotesController _controller;

        public QuotesControllerUnitTests()
        {
            _mockRepo = new Mock<IQuoteRepository>();
            _controller = new QuotesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetQuotesAsync_AllQuotes_Returns200StatusCodeWithQuotesCollection()
        {
            var filter = new QuoteFilter();

            var quotes = new List<Quote> { new Quote(), new Quote() };

            _mockRepo
                .Setup(repo => repo.GetQuotesAsync(filter))
                .ReturnsAsync(quotes);

            var response = await _controller.GetQuotesAsync(filter) as ObjectResult;

            var data = response.Value as List<Quote>;

            _mockRepo.Verify(repo => repo.GetQuotesAsync(It.IsAny<QuoteFilter>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            data.Should().NotBeNull();
            data.Should().BeOfType<List<Quote>>();
            data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetQuotesAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            var filter = new QuoteFilter();

            _mockRepo
                .Setup(repo => repo.GetQuotesAsync(filter))
                .Throws(new Exception());

            var response = await _controller.GetQuotesAsync(filter) as ObjectResult;

            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetQuotesAsync(It.IsAny<QuoteFilter>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }


        [Fact]
        public async Task GetQuoteByIdAsync_ValidId_Returns200StatusCodeWithQuote()
        {
            var quoteId = "62b7d5506c1b407771829926";

            _mockRepo
                .Setup(repo => repo.GetQuoteByIdAsync(quoteId))
                .ReturnsAsync(new Quote());

            var response = await _controller.GetQuoteByIdAsync(quoteId) as ObjectResult;

            var character = response.Value;

            _mockRepo.Verify(repo => repo.GetQuoteByIdAsync(It.IsAny<string>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            character.Should().NotBeNull();
            character.Should().BeOfType<Quote>();
        }

        public async Task GetQuoteByIdAsync_ValidIdForNonExistentQuote_Returns404StatusCodeWithProblemDetails()
        {
            var quoteId = "62b7d5506c1b407771829926";

            _mockRepo
                .Setup(repo => repo.GetQuoteByIdAsync(quoteId))
                .ReturnsAsync(null as Quote);

            var response = await _controller.GetQuoteByIdAsync(quoteId) as ObjectResult;

            var quote = response.Value;

            _mockRepo.Verify(repo => repo.GetQuoteByIdAsync(It.IsAny<String>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

            quote.Should().NotBeNull();
            quote.Should().BeOfType<Quote>();
        }

        [Fact]
        public async Task GetQuoteByIdAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            var quoteId = "62b7d5506c1b407771829926";

            _mockRepo
                .Setup(repo => repo.GetQuoteByIdAsync(quoteId))
                .Throws(new Exception());

            var response = await _controller.GetQuoteByIdAsync(quoteId) as ObjectResult;

            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetQuoteByIdAsync(It.IsAny<string>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }
    }
}
