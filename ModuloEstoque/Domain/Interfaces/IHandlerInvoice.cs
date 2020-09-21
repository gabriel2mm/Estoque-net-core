using Stock.Domain.Enumerators;
using Stock.Domain.Models;

namespace Domain.Interfaces
{
    public interface IHandlerInvoice
    {
        public IHandlerInvoice SetNext(IHandlerInvoice handler);
        public Invoice Handle(TransitionType type);
    }
}
