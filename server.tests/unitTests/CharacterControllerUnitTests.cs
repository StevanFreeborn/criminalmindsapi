using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using System.Net;

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

        [Fact]
        public async Task GetCharactersAsync_AllCharacters_Returns200StatusCodeWithCharactersCollection()
        {
            var filter = new CharacterFilter();

            var characters = new List<Character> {new Character(), new Character()};

            _mockRepo
                .Setup(repo => repo.GetCharactersAsync(filter))
                .ReturnsAsync(characters);

            var response = await _controller.GetCharactersAsync(filter) as ObjectResult;
            var data = response.Value as List<Character>;
            
            _mockRepo.Verify(repo => repo.GetCharactersAsync(It.IsAny<CharacterFilter>()), Times.Once());
            
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            data.Should().NotBeNull();
            data.Should().BeOfType<List<Character>>();
            data.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetCharactersAsync_RepoThrowsException_Returns500StatusCodeWithProblemDetails()
        {
            var filter = new CharacterFilter();

            _mockRepo
                .Setup(repo => repo.GetCharactersAsync(filter))
                .Throws(new Exception());

            var response = await _controller.GetCharactersAsync(filter) as ObjectResult;

            var details = response.Value;

            _mockRepo.Verify(repo => repo.GetCharactersAsync(It.IsAny<CharacterFilter>()), Times.Once());

            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectResult>();
            response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }
    }
}
