using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain.Enumerators
{
    public enum InvoiceType
    {
        COMPRA,
        VENDA,
        SAIDA_INTERNA,
        SAIDA_ESTOQUE,
        SAIDA_DEVOLUCAO,
        SAIDA_PERDA
    }
}
