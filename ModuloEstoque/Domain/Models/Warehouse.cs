using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Deposito")]
    public class Warehouse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column("DepositoCode")]
        public String WarehouseCode { get; set; }

        [Column("DeTerceiros")]
        [DefaultValue(true)]
        public bool ThirdParty { get; set; }

        [ForeignKey("ProductsControlWareHouseIDs")]
        public ICollection<ProductManagement> ProductManagements { get; set; }

        [ForeignKey("LocationWareHouseId")]
        public Location Location { get; set; }

        public Warehouse Clone()
        {
            return this.MemberwiseClone() as Warehouse;
        }

        public void Copy(Warehouse warehouse) {
            this.WarehouseCode = warehouse.WarehouseCode;
            this.Location = warehouse.Location;
            this.ProductManagements = warehouse.ProductManagements;
            this.ThirdParty = warehouse.ThirdParty;
        }
    }
}
