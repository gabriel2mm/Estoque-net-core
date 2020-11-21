using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System.Linq;

namespace Stock.Infrastructure.Repositories
{
    public class BillsToPayRepository : Repository<BillsToPay>
    {
        public BillsToPayRepository(EfContext efContext) : base(context: efContext)
        {
        }
        public override IQueryable<BillsToPay> GetAll()
        {
            return base.GetAll().Include(b => b.BillTransactions);
        }
        public override void Add(BillsToPay obj)
        {
            _context.Set<BillsToPay>().Add(obj);
        }

        public override BillsToPay Find(params object[] key)
        {
            BillsToPay order = base.Find(key);
            if (order != null)
            {
                _context.Entry(order).Collection(b => b.BillTransactions).Load();
            }
            return order;
        }

        public override void Update(BillsToPay obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
