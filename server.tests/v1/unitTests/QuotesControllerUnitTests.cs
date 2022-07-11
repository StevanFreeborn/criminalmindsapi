using Moq;
using server.Controllers.v1;
using server.Persistence.Repositories;

namespace server.tests.v1.unitTests
{
    public class QuotesControllerUnitTests
    {
        private readonly Mock<IQuoteRepository> _mockRepo;
        private readonly QuotesController _controller;

        public QuotesControllerUnitTests()
        {
            _mockRepo = new Mock<IQuoteRepository>();
            _controller = new QuotesController(_mockRepo.Object);
        }
    }
}
