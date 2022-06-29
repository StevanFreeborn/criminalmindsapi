using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using System.Net;

namespace tests.controllers
{
    public class CharactersControllerTests
    {
        private readonly Mock<ICharacterRepository> _mockRepo;
        private readonly CharactersController _controller;

        public CharactersControllerTests()
        {
            _mockRepo = new Mock<ICharacterRepository>();
            _controller = new CharactersController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetCharactersAsync_NoFilterNoCharacters_Returns200StatusWithEmptyCollection()
        {
            var filter = new CharacterFilter();

            _mockRepo
                .Setup(repo => repo.GetCharactersAsync(filter))
                .ReturnsAsync( new List<Character>());

            var result = await _controller.GetCharactersAsync(filter) as ObjectResult;
            var data = result?.Value as List<Character>;

            _mockRepo.Verify(c => c.GetCharactersAsync(filter), Times.Once());
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Character>>(data);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Empty(data);
        }
    }
}
