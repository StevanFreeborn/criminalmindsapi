using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using System.Net;

namespace server.tests.unitTests
{
    public class EpisodesControllerUnitTests
    {
        private readonly Mock<IEpisodeRepository> _mockRepo;
        private readonly EpisodesController _controller;

        public EpisodesControllerUnitTests()
        {
            _mockRepo = new Mock<IEpisodeRepository>();
            _controller = new EpisodesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetEpisodesAsync_AllEpisodes_Returns200StatusCodeWithEpisodesCollection()
        {
            var filter = new EpisodeFilter();

            var episodes = new List<Episode> { new Episode(), new Episode() };

            _mockRepo
                .Setup(repo => repo.GetEpisodesAsync(filter))
                .ReturnsAsync(episodes);

            var response = await _controller.GetEpisodesAsync(filter) as ObjectResult;

            var data = response.Value as List<Episode>;

            _mockRepo.Verify(repo => repo.GetEpisodesAsync(It.IsAny<EpisodeFilter>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            data.Should().NotBeNull();
            data.Should().BeOfType<List<Episode>>();
            data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetEpisodesAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            var filter = new EpisodeFilter();

            _mockRepo
                .Setup(repo => repo.GetEpisodesAsync(filter))
                .Throws(new Exception());

            var response = await _controller.GetEpisodesAsync(filter) as ObjectResult;
            
            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetEpisodesAsync(It.IsAny<EpisodeFilter>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }
    }
}
