using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using Stock.Utils.Helpers;

namespace Stock.Application.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<OrdersShowcases> _orderShowCaseRepository;
        private readonly IRepository<ProductManagement> _productManagementRepository;
        private readonly IRepository<ProductTransition> _transitionRepository;
        public OrderController(IRepository<Order> repository, IRepository<ProductTransition> transitionRepository, IRepository<ProductManagement> productManagementRepository, IRepository<OrdersShowcases> orderShowCaseRepository)
        {
            _repository = repository;
            _productManagementRepository = productManagementRepository;
            _transitionRepository = transitionRepository;
            _orderShowCaseRepository = orderShowCaseRepository;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public Order Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/orders
        [HttpPost]
        public String Post([FromBody] Order order)
        {
            foreach (Showcase showcase in order.Products)
            {
                Product product = showcase.ProductManagement.Product;
                Warehouse warehouse = showcase.ProductManagement.Warehouse;

                if (product != null)
                {
                    IHandlerInvoice chainInvoiceInput = new ChainInputInvoice();
                    IHandlerInvoice chainInvoiceOutput = new ChainOutputInvoice();
                    IHandlerInvoice chainInvoiceInternalOutput = new ChainInternalOutputInvoice();
                    IHandlerInvoice chainInvoiceOutOfStock = new ChainOutOfStockInvoice();
                    IHandlerInvoice chainInvoiceReturnOutput = new ChainReturnOutputInvoice();
                    IHandlerInvoice chainInvoiceLossOutput = new ChainLossOutputInvoice();

                    chainInvoiceInput
                        .SetNext(chainInvoiceOutput)
                        .SetNext(chainInvoiceInternalOutput)
                        .SetNext(chainInvoiceOutOfStock)
                        .SetNext(chainInvoiceReturnOutput)
                        .SetNext(chainInvoiceLossOutput);

                    Invoice invoice = chainInvoiceInput.Handle(Domain.Enumerators.TransitionType.OUTPUT);

                    ProductTransition productTransition = new ProductTransition()
                    {
                        Product = product,
                        Warehouse = warehouse,
                        TransitionType = Domain.Enumerators.TransitionType.OUTPUT,
                        Invoice = invoice,
                        Transition = DateTime.Now,
                        Quantity = showcase.Quantidade,
                        UnitCost = product.Price
                    };

                    ProductManagement productManagement = showcase.ProductManagement;
                    if (productManagement != null)
                    {
                        if (productManagement.Amount >= productTransition.Quantity)
                        {
                            productManagement.Amount -= productTransition.Quantity;
                            productManagement.TotalCost -= productManagement.AverageCost * productTransition.Quantity;
                            productManagement.AverageCost = 0;

                            _productManagementRepository.Update(productManagement);
                            _productManagementRepository.SaveAll();

                            _transitionRepository.Add(productTransition);
                            _transitionRepository.SaveAll();

                            order.OrderCode = DateTime.Now.Ticks.ToString();
                        }
                    }
                }
            }
            order.Id = new Guid();
            _repository.Add(order);
            _repository.SaveAll();

            foreach (Showcase s in order.Products)
            {
                OrdersShowcases ordersShowcases = new OrdersShowcases()
                {
                    Order = order, 
                    OrderId = order.Id,
                    ShowCase = s,
                    ShowcaseId = s.Id
                };

                _orderShowCaseRepository.Add(ordersShowcases);
                
            }

            _orderShowCaseRepository.SaveAll();

            return order.OrderCode;
        }

        // PUT api/orders/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Order value)
        {
            Order order = _repository.Find(id);
            if (order != null)
            {
                order.Copy(value);
                _repository.Update(order);
                _repository.SaveAll();
            }
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
