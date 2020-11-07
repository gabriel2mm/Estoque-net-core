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
    public class LocationControllerTests
    {
        private readonly IRepository<Location> _repository;
        private readonly LocationController _controller;

        public LocationControllerTests()
        {
            _repository = new LocationMockRepository();
            _controller = new LocationController(_repository);
        }

        [Fact]
        public void Get()
        {
            var locations = _controller.Get();
            Assert.IsType<List<Location>>(locations);
        }

        [Fact]
        public void Get_With_Id()
        {
            var location = _controller.Get(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            Assert.IsType<Location>(location);
        }

        [Fact]
        public void Post()
        {
            Location location = new Location()
            {
                Id = new Guid()
            };
            _controller.Post(location);

            int count = _repository.GetAll().ToList().Count();

            Assert.Equal<int>(2, count);
        }

        [Fact]
        public void Put()
        {
            _controller.Put(new Guid(), new Location());
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
