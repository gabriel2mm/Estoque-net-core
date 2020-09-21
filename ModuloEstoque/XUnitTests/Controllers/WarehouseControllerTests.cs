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
    public class WarehouseControllerTests
    {
        private readonly IRepository<Warehouse> _repository;
        private readonly WarehouseController _controller;

        public WarehouseControllerTests()
        {
            _repository = new WarehouseMockRepository();
            _controller = new WarehouseController(_repository);
        }

        [Fact]
        public void Get()
        {
            var warehouses = _controller.Get();
            Assert.IsType<List<Warehouse>>(warehouses);
        }

        [Fact]
        public void Get_With_Id()
        {
            var warehouse = _controller.Get(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            Assert.IsType<Warehouse>(warehouse);
        }

        [Fact]
        public void Post()
        {
            Warehouse warehouse = new Warehouse()
            {
                Id = new Guid()
            };
            _controller.Post(warehouse);

            int count = _repository.GetAll().ToList().Count();

            Assert.Equal<int>(2, count);
        }

        [Fact]
        public void Put()
        {
            _controller.Put(new Guid(), new Warehouse());
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
