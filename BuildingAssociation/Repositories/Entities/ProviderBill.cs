using Repositories.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class ProviderBill : BaseEntity
    {
        [ForeignKey("Provider")]
        public long? ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        [ForeignKey("Mansion")]
        public long? MansionId { get; set; }
        public virtual Mansion Mansion { get; set; }

        [Required]
        public double Units { get; set; }

        public double? ProviderUnitPrice { get; set; }

        public double Other { get; set; }

        public bool Paid { get; set; }

        public DateTime? CreationDate { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
    }
}
