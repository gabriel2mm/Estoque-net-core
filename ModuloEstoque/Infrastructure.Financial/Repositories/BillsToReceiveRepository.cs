using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System.Linq;

namespace Stock.Infrastructure.Repositories
{
    public class BillsToReceiveRepository : Repository<BillsToReceive>
    {
        public BillsToReceiveRepository(EfContext efContext) : base(context: efContext)
        {
        }
        public override IQueryable<BillsToReceive> GetAll()
        {
            return base.GetAll().Include(b => b.BillTransactions);
        }
        public override void Add(BillsToReceive obj)
        {
            _context.Set<BillsToReceive>().Add(obj);
        }

        public override BillsToReceive Find(params object[] key)
        {
            BillsToReceive order = base.Find(key);
            if (order != null)
            {
                _context.Entry(order).Collection(b => b.BillTransactions).Load();
            }
            return order;
        }

        public override void Update(BillsToReceive obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
