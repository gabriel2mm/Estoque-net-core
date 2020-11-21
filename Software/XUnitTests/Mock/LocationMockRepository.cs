using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stock.XUnitTests.Mock
{
    public class LocationMockRepository : MockRepository<Location>
    {
        protected new readonly ICollection<Location> context;


        public LocationMockRepository()
        {
            context = new List<Location>()
            {
                new Location(){ 
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), 
                    CEP = "82300000",
                    City = "Curitiba", 
                    Complement = "bloco 1 apto 1",
                    District = "Bairro",
                    Name = "Location 1",
                    Number = 301,
                    State = "PR",
                    Street = "Rua 1",
                }
            };
        }

        public override IQueryable<Location> GetAll()
        {
            return context.AsQueryable();
        }

        public override Location Find(params object[] key)
        {
            return context.Where(o => o.Id.Equals(new Guid(key[0].ToString()))).FirstOrDefault();
        }

        public override void Add(Location obj)
        {
            context.Add(obj);
        }

        public override void Update(Location obj)
        {
            context.Remove(obj);
            obj.Name = "Teste 1";
            context.Add(obj);
        }

        public override void Delete(Func<Location, bool> predicate)
        {
            Location location = context.Where(predicate).FirstOrDefault();
            context.Remove(location);
        }
    }
}
