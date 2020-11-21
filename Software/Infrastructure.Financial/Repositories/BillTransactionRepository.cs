using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System.Linq;

namespace Stock.Infrastructure.Repositories
{
    public class BillTransactionRepository : Repository<BillTransaction>
    {
        public BillTransactionRepository(EfContext efContext) : base(context: efContext)
        {
        }

        public override IQueryable<BillTransaction> GetAll()
        {
            return base.GetAll().Include(b => b.BillsToPay).Include(b => b.BillsToReceive);
        }

        public override void Add(BillTransaction obj)
        {
            _context.Attach(obj.BillsToPay);
            _context.Attach(obj.BillsToReceive);
            _context.Add<BillTransaction>(obj);
        }

        public override BillTransaction Find(params object[] key)
        {
            BillTransaction bill = base.Find(key);
            if (bill != null)
            {
                _context.Entry(bill).Reference(b => b.BillsToPay).Load();
                _context.Entry(bill).Reference(b => b.BillsToReceive).Load();
            }
            return bill;
        }

        public override void Update(BillTransaction obj)
        {
            _context.Entry(obj.BillsToPay).State = EntityState.Modified;
            _context.Entry(obj.BillsToReceive).State = EntityState.Modified;
            _context.Entry(obj).State = EntityState.Modified;
            _context.Set<BillTransaction>().Update(obj);
        }
    }
}
