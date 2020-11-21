using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System.Linq;

namespace Stock.Infrastructure.Repositories
{
    public class ProviderRepository : Repository<Provider> 
    {
        protected new readonly EfContext _context;
        public ProviderRepository(EfContext context): base(context)
        {
            _context = context;
        }

        public override IQueryable<Provider> GetAll()
        {
            return base.GetAll().Include(p => p.Products);
        }

        public override Provider Find(params object[] key)
        {
            Provider provider = base.Find(key);
            if (provider != null)
            {
                _context.Entry(provider).Collection<ProductProvider>(p => p.Products).Load();
            }
            return provider;
        }

        public override void Update(Provider obj)
        {
            _context.Entry(obj.Products).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
