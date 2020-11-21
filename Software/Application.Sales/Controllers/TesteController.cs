using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;

namespace Stock.Application.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly EfContext _context;
        public TesteController()
        {
            EfContextFactory factory = new EfContextFactory();
            _context = factory.CreateDbContext(null);
        }
        [HttpGet]
        public void Get()
        {
            BranchOffice branchOffice = new BranchOffice()
            {
                Id = new Guid(),
                Name = "Filial 1",
            };

            BranchOffice branchOffice2 = new BranchOffice()
            {
                Id = new Guid(),
                Name = "Filial 2",
            };

           

            Location location = new Location()
            {
                Id = new Guid(),
                CEP = "81230170",
                City = "Curitiba",
                Name = "Depósito 1",
                Complement = "bloco 1 apto 32",
                District = "Campo comprido",
                Number = 3651,
                State = "PR",
                Street = "Renato polatti",
            };

            Location location2 = new Location()
            {
                Id = new Guid(),
                CEP = "82310260",
                City = "Curitiba",
                Name = "Depósito 2",
                Complement = null,
                District = "Orleans",
                Number = 406,
                State = "PR",
                Street = "Luiz foggiatto",
            };


            Warehouse warehouse = new Warehouse()
            {
                WarehouseCode = "DP1",
                Id = new Guid(),
                BranchOffice = branchOffice,
                Location = location,
                ThirdParty = false,
            };

            Warehouse warehouse2 = new Warehouse()
            {
                WarehouseCode = "DP2",
                Id = new Guid(),
                BranchOffice = branchOffice2,
                Location = location2,
                ThirdParty = true,
            };

            Product p1 = new Product()
            {
                Id = new Guid(),
                Input = DateTime.Now,
                Measure = "un",
                Price = 199,
                Name = "Camiseta time real madrid",
                ProductCode = "CS1",
                Unit = "un",
            };

            Product p2 = new Product()
            {
                Id = new Guid(),
                Input = DateTime.Now,
                Measure = "un",
                Price = 99,
                Name = "Camiseta time Barcelona",
                ProductCode = "CS2",
                Unit = "un",
            };

            Product p3 = new Product()
            {
                Id = new Guid(),
                Input = DateTime.Now,
                Measure = "un",
                Price = 250,
                Name = "Camiseta time Liverpool",
                ProductCode = "CS3",
                Unit = "un",
            };

            ProductManagement productManagement = new ProductManagement()
            {
                Product = p1,
                Amount = 16,
                AverageCost = p1.Price / 16,
                TotalCost = p1.Price * 16,
                Id = new Guid()
            };
            ProductManagement productManagement2 = new ProductManagement()
            {
                Product = p2,
                Amount = 4,
                AverageCost = p2.Price / 4,
                TotalCost = p2.Price * 4,
                Id = new Guid()
            };
            ProductManagement productManagement3 = new ProductManagement()
            {
                Product = p3,
                Amount = 28,
                AverageCost = p3.Price / 28,
                TotalCost = p3.Price * 28,
                Id = new Guid()
            };

            Provider provider = new Provider()
            {
                Name = "Nike",
                Id = new Guid(),    
            };

            Provider provider2 = new Provider()
            {
                Name = "Addidas",
                Id = new Guid(),
            };

            Provider provider3 = new Provider()
            {
                Name = "Puma",
                Id = new Guid(),
            };

            ProductProvider productProvider = new ProductProvider()
            {
                Product = p1,
                ProductId = p1.Id,
                Provider = provider,
                ProviderId = provider.Id
            };

            ProductProvider productProvider2 = new ProductProvider()
            {
                Product = p2,
                ProductId = p2.Id,
                Provider = provider2,
                ProviderId = provider2.Id
            };

            ProductProvider productProvider3 = new ProductProvider()
            {
                Product = p3,
                ProductId = p3.Id,
                Provider = provider3,
                ProviderId = provider3.Id
            };

            ProductTransition t1 = new ProductTransition()
            {
                Id = new Guid(),
                Product = p1,
                Quantity = 16,
                Transition = DateTime.Now,
                TransitionType = Domain.Enumerators.TransitionType.INPUT,
                UnitCost = p1.Price,
                Warehouse = warehouse,
            };

            ProductTransition t2 = new ProductTransition()
            {
                Id = new Guid(),
                Product = p3,
                Quantity = 8,
                Transition = DateTime.Now,
                TransitionType = Domain.Enumerators.TransitionType.INPUT,
                UnitCost = p3.Price,
                Warehouse = warehouse,
            };

            ProductTransition t3 = new ProductTransition()
            {
                Id = new Guid(),
                Product = p2,
                Quantity = 4,
                Transition = DateTime.Now,
                TransitionType = Domain.Enumerators.TransitionType.INPUT,
                UnitCost = p2.Price,
                Warehouse = warehouse2,
            };

            Showcase showcase = new Showcase()
            {
                Color = "blue",
                Description = "Camiseta real madrid",
                Image = "RealIMG.jpg",
                Quantidade = productManagement.Amount,
            };

            Showcase showcase2 = new Showcase()
            {
                Id = new Guid(),
                Color = "red",
                Description = "Camiseta barcelona",
                Image = "barcelonaIMG.jpg",
                Quantidade = productManagement2.Amount
            };

            Showcase showcase3 = new Showcase()
            {
                Color = "red",
                Description = "Camiseta liverpool",
                Image = "LiverpoolIMG.jpg",
                Quantidade = productManagement3.Amount
            };

            Campaign campaign = new Campaign()
            {
                Showcase = showcase,
                Id = new Guid(),
                EndDate = DateTime.Now.AddDays(35),
                StartDate = DateTime.Now.AddDays(-5),
                ImageBanner = "banner.JPG"
            };

            _context.Set<BranchOffice>().Add(branchOffice);
            _context.Set<BranchOffice>().Add(branchOffice2);
            _context.Set<Location>().Add(location);
            _context.Set<Location>().Add(location2);
            _context.Set<Warehouse>().Add(warehouse);
            _context.Set<Warehouse>().Add(warehouse2);
            _context.Set<Product>().Add(p1);
            _context.Set<Product>().Add(p2);
            _context.Set<Product>().Add(p3);
            _context.Set<ProductManagement>().Add(productManagement);
            _context.Set<ProductManagement>().Add(productManagement2);
            _context.Set<ProductManagement>().Add(productManagement3);
            _context.Set<Provider>().Add(provider);
            _context.Set<Provider>().Add(provider2);
            _context.Set<Provider>().Add(provider3);

            _context.Set<ProductProvider>().Add(productProvider);
            _context.Set<ProductProvider>().Add(productProvider2);
            _context.Set<ProductProvider>().Add(productProvider3);

            _context.Set<ProductTransition>().Add(t1);
            _context.Set<ProductTransition>().Add(t2);
            _context.Set<ProductTransition>().Add(t3);

            _context.Set<Showcase>().Add(showcase);
            _context.Set<Showcase>().Add(showcase2);
            _context.Set<Showcase>().Add(showcase3);

            _context.Set<Campaign>().Add(campaign);

            _context.SaveChanges();
        }
    }
}
