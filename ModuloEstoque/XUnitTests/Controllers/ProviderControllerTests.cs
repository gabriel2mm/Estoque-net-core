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
    public class ProviderControllerTests
    {
        private readonly IRepository<Provider> _repository;
        private readonly ProviderController _controller;

        public ProviderControllerTests()
        {
            _repository = new ProviderMockRepository();
            _controller = new ProviderController(_repository);
        }

        [Fact]
        public void Get()
        {
            var providers = _controller.Get();
            Assert.IsType<List<Provider>>(providers);
        }

        [Fact]
        public void Get_With_Id()
        {
            var provider = _controller.Get(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            Assert.IsType<Provider>(provider);
        }

        [Fact]
        public void Post()
        {
            Provider provider = new Provider()
            {
                Id = new Guid()
            };
            _controller.Post(provider);

            int count = _repository.GetAll().ToList().Count();

            Assert.Equal<int>(2, count);
        }

        [Fact]
        public void Put()
        {
            _controller.Put(new Guid(), new Provider());
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
