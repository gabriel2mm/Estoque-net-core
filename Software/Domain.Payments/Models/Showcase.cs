using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Vitrine")]
    public class Showcase
    {
        [Key]
        public Guid Id { get; set; }
        [Column("gerenciadorProdutos")]
        public ProductManagement ProductManagement { get; set; }
        [Column("descrição")]
        public String Description { get; set; }
        [Column("imagem")]
        public String Image { get; set; }
        [Column("cor")]
        public String Color { get; set; }

        [DefaultValue(1)]
        [Column("quantidade")]
        public int Quantidade { get; set; }
        [Column("PedidosVitrines")]
        public ICollection<OrdersShowcases> OrderShowcases { get; set; }

        public Showcase Clone()
        {
            return this.MemberwiseClone() as Showcase;
        }

        public void Copy(Showcase showcase)
        {
            this.ProductManagement = showcase.ProductManagement;
            this.Image = showcase.Image;
            this.Description = showcase.Description;
            this.Color = showcase.Color;
            this.Quantidade = showcase.Quantidade;
        }
    }
}
