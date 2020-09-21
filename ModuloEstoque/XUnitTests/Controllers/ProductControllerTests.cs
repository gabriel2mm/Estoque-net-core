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
    public class ProductControllerTests
    {
        private readonly IRepository<Product> _repository;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _repository = new ProductMockRepository();
            _controller = new ProductController(_repository);
        }

        [Fact]
        public void Get()
        {
            var products = _controller.Get();
            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public void Get_With_Id()
        {
            var product = _controller.Get(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            Assert.IsType<Product>(product);
        }

        [Fact]
        public void Post()
        {
            Product product = new Product()
            {
                Id = new Guid()
            };
            _controller.Post(product);

            int count = _repository.GetAll().ToList().Count();

            Assert.Equal<int>(2, count);
        }

        [Fact]
        public void Put()
        {
            _controller.Put(new Guid(), new Product());
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
