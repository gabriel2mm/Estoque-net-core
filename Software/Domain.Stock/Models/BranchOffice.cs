using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Filial")]
    public class BranchOffice
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Nome")]
        public String Name { get; set; }
        public ICollection<Warehouse> Warehouses { get; set; }

        public BranchOffice Clone()
        {
            return this.MemberwiseClone() as BranchOffice;
        }
        public void Copy(BranchOffice branchOffice)
        {
            this.Name = branchOffice.Name;
        }
    }
}
