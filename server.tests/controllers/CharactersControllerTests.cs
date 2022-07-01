using Microsoft.AspNetCore.Mvc;
using Moq;
using server.Controllers.v1;
using server.Models;
using server.Persistence.Repositories;
using System.ComponentModel.DataAnnotations;
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
        public async Task GetCharactersAsync_NoFilterParamsNoCharacters_Returns200StatusWithEmptyCollection()
        {
            var filter = new CharacterFilter();

            _mockRepo
                .Setup(repo => repo.GetCharactersAsync(filter))
                .ReturnsAsync( new List<Character>());

            var result = await _controller.GetCharactersAsync(filter) as ObjectResult;
            var data = result?.Value as List<Character>;

            _mockRepo.Verify(repo => repo.GetCharactersAsync(It.IsAny<CharacterFilter>()), Times.Once());
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Character>>(data);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Empty(data);
        }

        [Fact]
        public async Task GetCharactersAsync_NoFilterParamsWithCharacters_Returns200StatusWithCharactersCollection()
        {
            var filter = new CharacterFilter();

            var character = new Character
            {
                Id = "",
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
            var data = result.Value;

            _mockRepo.Verify(repo => repo.GetCharactersAsync(It.IsAny<CharacterFilter>()), Times.Once());
            Assert.IsType<ObjectResult>(result);
            Assert.IsType<ProblemDetails>(data);
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }
    }
}
