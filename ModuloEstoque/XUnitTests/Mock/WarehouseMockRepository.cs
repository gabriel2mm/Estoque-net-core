using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.XUnitTests.Mock
{
    public class WarehouseMockRepository : MockRepository<Warehouse>
    {
        protected new readonly ICollection<Warehouse> context;


        public WarehouseMockRepository()
        {
            context = new List<Warehouse>()
            {
                new Warehouse(){ 
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), 
                    Location = new Location(),
                    ThirdParty = true,
                    ProductManagements = null,
                    WarehouseCode = "101B"
                }
            };
        }

        public override IQueryable<Warehouse> GetAll()
        {
            return context.AsQueryable();
        }

        public override Warehouse Find(params object[] key)
        {
            return context.Where(o => o.Id.Equals(new Guid(key[0].ToString()))).FirstOrDefault();
        }

        public override void Add(Warehouse obj)
        {
            context.Add(obj);
        }

        public override void Update(Warehouse obj)
        {
            context.Remove(obj);
            obj.WarehouseCode = "Teste 1";
            context.Add(obj);
        }

        public override void Delete(Func<Warehouse, bool> predicate)
        {
            Warehouse warehouse = context.Where(predicate).FirstOrDefault();
            context.Remove(warehouse);
        }
    }
}
