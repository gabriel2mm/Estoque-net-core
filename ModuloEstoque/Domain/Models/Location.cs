using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Domain.Models
{
    [Table("Local")]
    public class Location
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Nome")]
        [Required]
        [StringLength(maximumLength: 20)]
        public String Name { get; set; }
        [Column("UF")]
        [StringLength(maximumLength: 2)]
        public String State { get; set; }
        [Column("Cidade")]
        public String City { get; set; }
        [Column("Bairro")]
        public String District { get; set; }
        [Column("Endereço")]
        [StringLength(maximumLength: 50)]
        public String Street { get; set; }
        [Column("CEP")]
        [StringLength(maximumLength: 8)]
        public String CEP { get; set; }
        [Column("Numero")]
        [DefaultValue(0)]
        public int Number { get; set; }
        [Column("Complemento")]
        [StringLength(maximumLength: 20)]
        public String Complement { get; set; }
        [ForeignKey("WareHousesID")]
        public ICollection<Warehouse> Warehouses { get; set; }
        
        public Location Clone()
        {
            return this.MemberwiseClone() as Location;
        }

        public void Copy(Location location)
        {
            this.CEP = location.CEP;
            this.City = location.City;
            this.Complement = location.Complement;
            this.District = location.District;
            this.Name = this.Name;
            this.Number = this.Number;
            this.State = this.State;
            this.Street = this.Street;
            this.Warehouses = this.Warehouses;
        }

    }

}

