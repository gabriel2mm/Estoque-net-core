using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.XUnitTests.Mock
{
    public class ProductManagementMockRepository : MockRepository<ProductManagement>
    {
        protected new readonly ICollection<ProductManagement> context;


        public ProductManagementMockRepository()
        {
            context = new List<ProductManagement>()
            {
                new ProductManagement(){
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Amount = 10,
                    AverageCost = 10,
                    Product = new Product(),
                    TotalCost = 20,
                    Warehouse = new Warehouse()
                }
            };
        }

        public override IQueryable<ProductManagement> GetAll()
        {
            return context.AsQueryable();
        }

        public override ProductManagement Find(params object[] key)
        {
            return context.Where(o => o.Id.Equals(new Guid(key[0].ToString()))).FirstOrDefault();
        }

        public override void Add(ProductManagement obj)
        {
            context.Add(obj);
        }

        public override void Update(ProductManagement obj)
        {
            context.Remove(obj);
            obj.Amount = 999;
            context.Add(obj);
        }

        public override void Delete(Func<ProductManagement, bool> predicate)
        {
            ProductManagement productManagement = context.Where(predicate).FirstOrDefault();
            context.Remove(productManagement);
        }
    }
}
