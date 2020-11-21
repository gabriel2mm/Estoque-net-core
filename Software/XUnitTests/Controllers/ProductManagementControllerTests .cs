using Stock.Application.Controllers;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using Stock.XUnitTests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Stock.XUnitTests.Controllers
{
    public class ProductManagementControllerTests
    {
        private readonly IRepository<ProductManagement> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<ProductTransition> _productTransitionRepository;
        private readonly ProductManagementController _controller;

        public ProductManagementControllerTests()
        {
            _productRepository = new ProductMockRepository();
            _warehouseRepository = new WarehouseMockRepository();
            _repository = new ProductManagementMockRepository();
            _productTransitionRepository = new ProductTransitionMockRepository();
            _controller = new ProductManagementController(_repository, _productTransitionRepository, _productRepository, _warehouseRepository);
        }

        [Fact]
        public void Get()
        {
            var productManagements = _controller.Get();
            Assert.IsType<List<ProductManagement>>(productManagements);
        }

        [Fact]
        public void Get_With_Id()
        {
            var productManagement = _controller.Get(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            Assert.IsType<ProductManagement>(productManagement);
        }

        [Fact]
        public void Post()
        {
            ProductTransition productTransition = new ProductTransition()
            {
                Product = new Product()
                {
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Input = DateTime.Now,
                    Measure = "10",
                    Unit = "kg",
                    Name = "produto 1",
                    Output = DateTime.Now,
                    Price = 10,
                    ProductCode = "101A",
                    Providers = null
                },
                Warehouse = new Warehouse()
                {
                    Id = new System.Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Location = new Location(),
                    ThirdParty = true,
                    ProductManagements = null,
                    WarehouseCode = "101B"
                },
                Invoice = new Invoice()
            };

            _controller.Post(productTransition);

            int count = _repository.GetAll().ToList().Count();

            Assert.Equal<int>(2, count);
        }

        [Fact]
        public void Put()
        {
            _controller.Put(new Guid(), new ProductManagement());
            Assert.True(true);
        }

        [Fact]
        public void Delete()
        {
            _controller.Delete(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            int count = _repository.GetAll().ToList().Count();

            Assert.Equal<int>(0, count);
        }
    }
}
