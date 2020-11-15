using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Models
{
    [Table("Campanha")]
    public class Campaign
    {
        [Key]
        public Guid Id { get; set; }
        
        public String ImageBanner { get; set; }
        public Showcase Showcase { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Campaign Clone()
        {
            return this.MemberwiseClone() as Campaign;
        }

        public void Copy(Campaign campaign)
        {
            this.Showcase = campaign.Showcase;
            this.StartDate = campaign.StartDate;
            this.EndDate = campaign.EndDate;
            this.ImageBanner = campaign.ImageBanner;
        }
    }
}
