using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class WaterConsumption : BaseEntity
    {
        [Required]
        public double HotWaterUnits { get; set; }

        [Required]
        public double ColdWaterUnits { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }
        public User User { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
