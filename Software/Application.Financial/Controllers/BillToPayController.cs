using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stock.Domain.Enumerators;
using Stock.Domain.Interfaces;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Financial.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BillToPayController : ControllerBase
    {
        private readonly IRepository<BillTransaction> _billTransactionRepository;
        private readonly IRepository<BillsToPay> _repository;
        public BillToPayController(IRepository<BillsToPay> repository, IRepository<BillTransaction> billTransactionRepository)
        {
            _repository = repository;
            _billTransactionRepository = billTransactionRepository;
        }

        [HttpPost]
        public void CreateBillToPay(BillsToPay billToPay)
        {
            _repository.Add(billToPay);
            _repository.SaveAll();

            this.CreateTransaction(billToPay, OperationType.CRIACAO, TransactionType.Saida);
        }

        [HttpPut("cancel/{id}")]
        public void CancelBillToPay(Guid Id)
        {
            BillsToPay billToPay = _repository.Find(Id);
            if (billToPay != null)
            {
                billToPay.Status = Stock.Domain.Enumerators.BillStatus.Cancelado;
                _repository.Update(billToPay);
                _repository.SaveAll();

                this.CreateTransaction(billToPay, OperationType.CANCELAMENTO, TransactionType.Saida);
            }
        }
        [HttpPut("pay/totalValue/{id}")]
        public void PayTotalValueBillToPay(Guid Id)
        {
            BillsToPay billToPay = _repository.Find(Id);
            if (billToPay != null)
            {
                billToPay.Status = Stock.Domain.Enumerators.BillStatus.Pago;
                _repository.Add(billToPay);
                _repository.SaveAll();

                this.CreateTransaction(billToPay, OperationType.PAGAMENTO, TransactionType.Saida);
            }
        }

        [HttpPut("pay/parcialValue/{id}")]
        public void PayParcialValueBillToPay(Guid Id, [FromBody] JObject obj)
        {
            BillsToPay billToPay = _repository.Find(Id);
            if (billToPay != null)
            {
                double value = obj.Value<double>("value");

                BillsToPay newBill = new BillsToPay()
                {
                    Id = new Guid(),
                    Value = billToPay.Value - value,
                    Beneficiary = billToPay.Beneficiary,
                    DueDate = billToPay.DueDate.AddDays(30),
                    Status = Stock.Domain.Enumerators.BillStatus.Aberto,
                    Invoice = billToPay.Invoice,
                    Number = new int(),
                    BeneficiaryNumber = billToPay.BeneficiaryNumber,
                    Discount = billToPay.Discount,
                    Fine = billToPay.Fine,
                    Interest = billToPay.Interest,
                };

                billToPay.Status = Stock.Domain.Enumerators.BillStatus.Pago;
                billToPay.NewTitle = newBill;
                _repository.Add(newBill);
                _repository.Update(billToPay);
                _repository.SaveAll();

                this.CreateTransaction(newBill, OperationType.CRIACAO, TransactionType.Saida);
                this.CreateTransaction(billToPay, OperationType.PAGAMENTO, TransactionType.Saida);
            }
        }

        [HttpPut("pay/creditCard/{id}")]
        public void PayCreditCardBillToPay(Guid Id, [FromBody] JObject obj)
        {
            BillsToPay billToPay = _repository.Find(Id);
            if (billToPay != null)
            {
                string beneficiary = obj.Value<String>("beneficiary");
                DateTime dueDate = new DateTime(long.Parse(obj.GetValue("dueDate")?.ToString()));

                BillsToPay newBill = new BillsToPay()
                {
                    Id = new Guid(),
                    Value = billToPay.Value,
                    Beneficiary = beneficiary,
                    DueDate = dueDate,
                    Status = Stock.Domain.Enumerators.BillStatus.Aberto,
                    Invoice = billToPay.Invoice,
                    Number = new int(),
                    BeneficiaryNumber = billToPay.BeneficiaryNumber,
                };

                billToPay.Status = Stock.Domain.Enumerators.BillStatus.Substituido;
                billToPay.NewTitle = newBill;
                _repository.Add(newBill);
                _repository.Update(billToPay);
                _repository.SaveAll();

                this.CreateTransaction(newBill, OperationType.CRIACAO, TransactionType.Saida);
                this.CreateTransaction(billToPay, OperationType.SUBSTITUICAO, TransactionType.Saida);
            }
        }

        [HttpPut("{id}")]
        public void UpdateBillToPay(Guid id, [FromBody] BillsToPay obj)
        {
            BillsToPay billToPay = _repository.Find(id);
            if (billToPay != null)
            {
                obj.Id = billToPay.Id;
                _repository.Update(obj);
                _repository.SaveAll();
            }

            this.CreateTransaction(billToPay, OperationType.ATUALIZADO, TransactionType.Saida);
        }

        private void CreateTransaction(BillsToPay billToPay, OperationType operationType, TransactionType transactionType)
        {
            BillTransaction transaction = new BillTransaction()
            {
                BillsToPay = billToPay,
                TransactionDate = DateTime.Now,
                Value = billToPay.Value,
                TransactionOperation = operationType,
                TransactionType = transactionType,
                Discount = billToPay.Discount,
                Fine = billToPay.Fine,
                BillsToReceive = null,
                Interest = billToPay.Interest,
                TransactionCode = "tr" + new Random().Next(0, 100).ToString() + DateTime.Now.ToString()
            };

            _billTransactionRepository.Add(transaction);
            _billTransactionRepository.SaveAll();
        }
    }
}
