using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Persistence.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly IDbContext _context;

        public QuoteRepository(IDbContext context)
        {
            _context = context;
        }

        // TODO: Implement GetQuotesAsync() method.
        public Task<List<Quote>> GetQuotesAsync(QuoteFilter filter)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement GetQuoteByIdAsync() method.
        public Task<Quote> GetQuoteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
