using MongoDB.Driver;
using server.Models;

namespace server.Persistence.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly IDbContext _context;

        public QuoteRepository(IDbContext context)
        {
            _context = context;
        }

        // TODO: Implement optional, but possible filters for query.
        public async Task<List<Quote>> GetQuotesAsync(QuoteFilter filter)
        {
            try 
            {
                var query = _context.Quotes.AsQueryable();
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // TODO: Implement GetQuoteByIdAsync() method.
        public Task<Quote> GetQuoteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
