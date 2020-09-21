using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stock.Domain.Enumerators;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using Stock.Domain.Models.DTO;
using Stock.Utils.Helpers;

namespace Stock.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductManagementController : ControllerBase
    {
        private readonly IRepository<ProductManagement> _repository;
        private readonly IRepository<ProductTransition> _transitionRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        public ProductManagementController(IRepository<ProductManagement> repository, IRepository<ProductTransition> transitionRepository, IRepository<Product> productRepository, IRepository<Warehouse> warehouseRepository)
        {
            _repository = repository;
            _transitionRepository = transitionRepository;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
        }

        // GET: api/productmanagement
        [HttpGet]
        public IEnumerable<ProductManagement> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/productmanagement/5
        [HttpGet("{id}")]
        public ProductManagement Get(Guid id)
        {
            return _repository.Find(id);
        }

        // POST api/productmanagement
        [HttpPost]
        public void Post([FromBody] ProductTransition productTransition)
        {
            Product product = _productRepository.Get(p => p.Id.Equals(productTransition.Product.Id) || p.ProductCode.Equals(productTransition.Product.ProductCode)).FirstOrDefault();
            Warehouse warehouse = _warehouseRepository.Get(w => w.Id.Equals(productTransition.Warehouse.Id) || w.WarehouseCode.Equals(productTransition.Warehouse.WarehouseCode)).FirstOrDefault();

            if (product != null && warehouse != null)
            {
                productTransition.Product = product;
                productTransition.Warehouse = warehouse;

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

                Invoice invoice = chainInvoiceInput.Handle(productTransition.TransitionType);

                ProductTransition pt = new ProductTransition();
                pt.Copy(productTransition);
                pt.Invoice = invoice;

                ProductManagement productManagement = _repository.Get(p => p.Product == product && p.Warehouse == warehouse).FirstOrDefault();
                if (productTransition.TransitionType == TransitionType.INPUT)
                {
                    if (productManagement == null)
                    {
                        productManagement = new ProductManagement()
                        {
                            Product = product,
                            Warehouse = warehouse,
                            TotalCost = productTransition.Quantity * productTransition.UnitCost,
                            Amount = productTransition.Quantity,
                            AverageCost = productTransition.UnitCost
                        };

                        _repository.Add(productManagement);
                        _repository.SaveAll();

                        
                      
                        _transitionRepository.Add(pt);
                        _transitionRepository.SaveAll();
                    }
                    else
                    {
                        productManagement.Amount += productTransition.Quantity;
                        productManagement.TotalCost += productTransition.Quantity * productTransition.UnitCost;
                        productManagement.AverageCost = productManagement.TotalCost / productManagement.Amount;

                        _repository.Update(productManagement);
                        _repository.SaveAll();

                        _transitionRepository.Add(pt);
                        _transitionRepository.SaveAll();
                    }
                }
                else
                {
                    if (productManagement != null)
                    {
                        if (productManagement.Amount >= productTransition.Quantity)
                        {
                            productManagement.Amount -= productTransition.Quantity;
                            productManagement.TotalCost -= productManagement.AverageCost * productTransition.Quantity;
                            productManagement.AverageCost = productManagement.TotalCost / productManagement.Amount;

                            _repository.Update(productManagement);
                            _repository.SaveAll();

                            _transitionRepository.Add(pt);
                            _transitionRepository.SaveAll();
                        }
                    }
                }
            }
        }

        // PUT api/productmanagement/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] ProductManagement value)
        {
            ProductManagement productManagement = _repository.Find(id);
            if (productManagement != null)
            {
                productManagement.Copy(value);
                _repository.Update(productManagement);
                _repository.SaveAll();
            }
        }

        // DELETE api/productmanagement/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Delete((p => id.Equals(p.Id)));
        }
    }
}
