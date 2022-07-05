using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using server.tests.Mocks;
using System.Net;
using System.Text.Json;
using Xunit.Abstractions;

namespace tests.controllers
{
    public class CharactersControllerTests
    {
        private readonly Mock<ICharacterRepository> _mockRepo;
        private readonly CharactersController _controller;
        private readonly ITestOutputHelper _output;

        public CharactersControllerTests(ITestOutputHelper output)
        {
            _mockRepo = new Mock<ICharacterRepository>();
            _controller = new CharactersController(_mockRepo.Object);
            _output = output;
        }

        [Fact]
        public async Task GetCharactersAsync_NoFilterParamsWithCharacters_Returns200StatusWithCharactersCollection()
        {
            var filter = new CharacterFilter();

            var character = new Character
            {
                Id = "62b7d5506c1b407771829926",
                FirstName = "Jason",
                LastName = "Gideon",
                ActorFirstName = "Mandy",
                ActorLastName = "Patinkin",
                Seasons = new int[] {1,2,3,10,15},
                FirstEpisode = "Extreme Aggressor",
                LastEpisode = "In Name and Blood",
                Image = "https://criminalmindsapi.stevanfreeborn.com/characters/jason-gideon.png",
                Bio = "Jason Gideon was a criminal profiler, formerly the Senior Supervisory Special Agent of the FBI's Behavioral Analysis Unit. At the beginning of Season Three, Gideon abruptly retired from the BAU due to emotional issues brought on by the murder of his girlfriend. His position is now held by his former partner and best friend David Rossi, who has held it to this day. In the Season Ten episode \"Nelson's Sparrow\", he was murdered by Donnie Mallick.",
            };

            var characters = new List<Character>
            {
                character
            };

            _mockRepo
                .Setup(repo => repo.GetCharactersAsync(filter))
                .ReturnsAsync(characters);

            var result = await _controller.GetCharactersAsync(filter) as ObjectResult;
            var data = result?.Value as List<Character>;

            _mockRepo.Verify(repo => repo.GetCharactersAsync(It.IsAny<CharacterFilter>()), Times.Once());
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Character>>(data);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Single(data);  
        }

        [Fact]
        public async Task GetCharactersAsync_RepoThrowsException_Returns500StatusWithProblemDetail()
        {
            var filter = new CharacterFilter();

            _mockRepo
                .Setup(repo => repo.GetCharactersAsync(filter))
                .Throws(new Exception());

            var result = await _controller.GetCharactersAsync(filter) as ObjectResult;
            var data = result?.Value;

            _mockRepo.Verify(repo => repo.GetCharactersAsync(It.IsAny<CharacterFilter>()), Times.Once());
            Assert.IsType<ObjectResult>(result);
            Assert.IsType<ProblemDetails>(data);
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task GetCharacterByIdAsync_ValidCharacterId_Returns200StatusWithCharacter()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task GetCharacterByIdAsync_InvalidCharacterId_Returns400StatusWithValidationProblemDetail()
        {
            var characterId = "1";

            _controller.ProblemDetailsFactory = new MockProblemDetailsFactory();

            var result = await _controller.GetCharacterByIdAsync(characterId) as ObjectResult;
            var data = result?.Value;

            string json = JsonSerializer.Serialize(result);

            _output.WriteLine(json);

            // Assert.IsType<ObjectResult>(result);
            // Assert.IsType<ValidationProblemDetails>();
        }

        [Fact]
        public async Task GetCharacterByIdAsync_RepoThrowsException_Returns500StatusWithProblemDetail()
        {
            var characterId = "62b7d5506c1b407771829938";

            _mockRepo
                .Setup(repo => repo.GetCharacterByIdAsync(characterId))
                .Throws(new Exception());

            var result = await _controller.GetCharacterByIdAsync(characterId) as ObjectResult;
            var data = result?.Value;

            _mockRepo.Verify(repo => repo.GetCharacterByIdAsync(It.IsAny<string>()), Times.Once());
            Assert.IsType<ObjectResult>(result);
            Assert.IsType<ProblemDetails>(data);
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }
    }
}
