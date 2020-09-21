using Domain.Interfaces;
using Stock.Domain.Enumerators;
using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Utils.Helpers
{
    public abstract class HandlerInvoiceBase : IHandlerInvoice
    {
        protected IHandlerInvoice _nextHandler;
        public virtual Invoice Handle(TransitionType type)
        {
            if (_nextHandler != null)
            {
                return this._nextHandler.Handle(type);
            }

            return null;
        }

        public virtual IHandlerInvoice SetNext(IHandlerInvoice handler)
        {
            _nextHandler = handler;

            return handler;
        }
    }
}
