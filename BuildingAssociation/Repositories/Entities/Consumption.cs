using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class Consumption
    {
        [Key]
        public long? ConsumptionId { get; set; }

        [ForeignKey("Provider")]
        public long? ProviderId { get; set; }
        public Provider Provider { get; set; }

        [Required]
        public double Units { get; set; }

        [Required]
        public bool Paid { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }
        public User User { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid Guid { get; set; }
    }
}
