using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models;
using Stock.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Infrastructure.Repositories
{
    public class BranchOfficeRepository : Repository<BranchOffice>
    {
        protected new readonly EfContext _context;
        public BranchOfficeRepository(EfContext context): base(context)
        {
            _context = context;
        }
    }
}
