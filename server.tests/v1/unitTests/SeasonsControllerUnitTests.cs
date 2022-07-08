using Moq;
using server.Controllers.v1;
using server.Persistence.Repositories;

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
            throw new NotImplementedException();
        }

        [Fact]
        public async Task GetSeasonsAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
