using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain.Enumerators
{
    public enum BillStatus
    {
        Aberto = 1, 
        Pago = 2,
        Cancelado = 3,
        Substituido = 4
    }
}
