using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class WaterConsumption : BaseEntity
    {
        [Required]
        public double KitchenUnits { get; set; }

        [Required]
        public double BathroomUnits { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
