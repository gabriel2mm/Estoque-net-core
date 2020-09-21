using Stock.Domain.Models;
using Stock.Infrastructure.Context;

namespace Stock.Infrastructure.Repositories
{
    public class ProviderRepository : Repository<Provider> 
    {
        protected new readonly EfContext _context;
        public ProviderRepository(EfContext context): base(context)
        {
            _context = context;
        }
    }
}
