using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using System.Net;

namespace server.tests.v1.UnitTests
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

        [Fact]
        public async Task GetEpisodeByNumberAsync_ValidEpisodeNumber_Returns200StatusWithEpisode()
        {
            var episodeNumber = 1;

            _mockRepo
                .Setup(repo => repo.GetEpisodeByNumberAsync(episodeNumber))
                .ReturnsAsync(new Episode());

            var response = await _controller.GetEpisodeByNumberAsync(episodeNumber) as ObjectResult;

            var episode = response.Value;

            _mockRepo.Verify(repo => repo.GetEpisodeByNumberAsync(It.IsAny<int>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            episode.Should().NotBeNull();
            episode.Should().BeOfType<Episode>();
        }

                [Fact]
        public async Task GetEpisodeByNumberAsync_ValidEpisodeNumberForNonExistentEpisode_Returns404StatusWithProblemDetails()
        {
            var episodeNumber = 3000;

            _mockRepo
                .Setup(repo => repo.GetEpisodeByNumberAsync(episodeNumber))
                .ReturnsAsync(null as Episode);

            var response = await _controller.GetEpisodeByNumberAsync(episodeNumber) as ObjectResult;

            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetEpisodeByNumberAsync(It.IsAny<int>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }

        [Fact]
        public async Task GetEpisodeByNumberAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            var episodeNumber = 1;

            _mockRepo
                .Setup(repo => repo.GetEpisodeByNumberAsync(episodeNumber))
                .Throws(new Exception());

            var response = await _controller.GetEpisodeByNumberAsync(episodeNumber) as ObjectResult;

            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetEpisodeByNumberAsync(It.IsAny<int>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }
    }
}
