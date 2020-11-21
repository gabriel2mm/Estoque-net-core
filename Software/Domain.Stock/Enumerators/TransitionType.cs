using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain.Enumerators
{
    public enum TransitionType
    {
        INPUT, 
        OUTPUT,
        INTERNAL_OUTPUT,
        OUT_OF_STOCK,
        RETURN_OUTPUT,
        LOSS_OUTPUT,
    }
}
