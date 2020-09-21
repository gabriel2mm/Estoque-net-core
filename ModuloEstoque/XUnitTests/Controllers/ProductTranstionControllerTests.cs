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
    public class ProductTransitionControllerTests
    {
        private readonly IRepository<ProductTransition> _repository;
        private readonly ProductTransitionController _controller;

        public ProductTransitionControllerTests()
        {
            _repository = new ProductTransitionMockRepository();
            _controller = new ProductTransitionController(_repository);
        }

        [Fact]
        public void Get()
        {
            var productTransitions = _controller.Get();
            Assert.IsType<List<ProductTransition>>(productTransitions);
        }

        [Fact]
        public void Get_With_Id()
        {
            var productTransition = _controller.Get(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            Assert.IsType<ProductTransition>(productTransition);
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
