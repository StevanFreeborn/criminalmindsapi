using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

        public async Task<List<Quote>> GetQuotesAsync(QuoteFilter filter)
        {
            try 
            {
                var query = _context.Quotes.AsQueryable();

                if (filter?.Season != null)
                {
                    query = query.Where(quote => quote.Season == filter.Season);
                }

                if (filter?.Episode != null)
                {
                    query = query.Where(quote => quote.Episode == filter.Episode);
                }

                if (filter?.TextKeyword != null)
                {
                    query = query.Where(quote => quote.Text!.ToLower().Contains(filter.TextKeyword.ToLower()));
                }

                if (filter?.Source != null)
                {
                    query = query.Where(quote => quote.Source!.ToLower().Contains(filter.Source.ToLower()));
                }

                if (filter?.Narrator != null)
                {
                    query = query.Where(quote => quote.Narrator!.ToLower().Contains(filter.Narrator.ToLower()));
                }

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Quote> GetQuoteByIdAsync(string id)
        {
            try
            {
                return await _context.Quotes.Find(quote => quote.Id == id).SingleOrDefaultAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
