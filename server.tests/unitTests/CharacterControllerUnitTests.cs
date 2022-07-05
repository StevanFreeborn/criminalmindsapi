using Moq;
using server.Controllers.v1;
using server.Persistence.Repositories;

namespace server.tests.unitTests
{
    public class CharacterControllerUnitTests
    {
        private readonly Mock<ICharacterRepository> _mockRepo;
        private readonly CharactersController _controller;

        public CharacterControllerUnitTests()
        {
            _mockRepo = new Mock<ICharacterRepository>();
            _controller = new CharactersController(_mockRepo.Object);
        }
    }
}
