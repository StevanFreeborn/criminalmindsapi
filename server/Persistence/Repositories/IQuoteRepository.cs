using server.Models;

namespace server.Persistence.Repositories
{
    public interface IQuoteRepository
    {
        Task<List<Quote>> GetQuotesAsync(QuoteFilter filter);
        Task<Quote> GetQuoteByIdAsync(int id);
    }
}
