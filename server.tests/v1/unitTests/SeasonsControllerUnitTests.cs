using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using System.Net;

namespace server.tests.v1.unitTests
{
    public class SeasonsControllerUnitTests
    {
        private readonly Mock<ISeasonRepository> _mockRepo;
        private readonly SeasonsController _controller;

        public SeasonsControllerUnitTests()
        {
            _mockRepo = new Mock<ISeasonRepository>();
            _controller = new SeasonsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetSeasonsAsync_AllSeasons_Returns200StatusCodeWithSeasonsCollection()
        {
            var seasons = new List<Season> { new Season(), new Season() };

            _mockRepo
                .Setup(repo => repo.GetSeasonsAsync())
                .ReturnsAsync(seasons);

            var response = await _controller.GetSeasonsAsync() as ObjectResult;

            var data = response.Value as List<Season>;

            _mockRepo.Verify(repo => repo.GetSeasonsAsync(), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            data.Should().NotBeNull();
            data.Should().BeOfType<List<Season>>();
            data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetSeasonsAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            _mockRepo
                .Setup(repo => repo.GetSeasonsAsync())
                .Throws(new Exception());

            var response = await _controller.GetSeasonsAsync() as ObjectResult;

            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetSeasonsAsync(), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_ValidSeasonNumber_Returns200StatusCodeWithSeason()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_ValidSeasonNumberForNonExistentSeason_Returns404StatusCodeWithProblemDetails()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            var seasonNumber = 1;

            _mockRepo
                .Setup(repo => repo.GetSeasonByNumberAsync(seasonNumber))
                .Throws(new Exception());

            var response = await _controller.GetSeasonByNumberAsync(seasonNumber) as ObjectResult;

            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetSeasonByNumberAsync(It.IsAny<int>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }
    }
}
