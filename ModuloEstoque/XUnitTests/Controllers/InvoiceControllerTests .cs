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
    public class InvoiceControllerTests
    {
        private readonly IRepository<Invoice> _repository;
        private readonly InvoiceController _controller;

        public InvoiceControllerTests()
        {
            _repository = new InvoiceMockRepository();
            _controller = new InvoiceController(_repository);
        }

        [Fact]
        public void Get()
        {
            var invoices = _controller.Get();
            Assert.IsType<List<Invoice>>(invoices);
        }

        [Fact]
        public void Get_With_Id()
        {
            var invoice = _controller.Get(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"));
            Assert.IsType<Invoice>(invoice);
        }

        [Fact]
        public void Post()
        {
            Invoice invoice = new Invoice()
            {
                Id = new Guid()
            };
            _controller.Post(invoice);

            int count = _repository.GetAll().ToList().Count();

            Assert.Equal<int>(2, count);
        }

        [Fact]
        public void Put()
        {
            _controller.Put(new Guid(), new Invoice());
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
