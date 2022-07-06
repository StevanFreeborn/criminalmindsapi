using Moq;
using server.Controllers.v1;
using server.Persistence.Repositories;

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
    }
}
