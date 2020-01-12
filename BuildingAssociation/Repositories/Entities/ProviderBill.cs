using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class ProviderBill : BaseEntity
    {
        [ForeignKey("Provider")]
        public long? ProviderId { get; set; }
        public Provider Provider { get; set; }

        [Required]
        public int Units { get; set; }

        public bool Paid { get; set; }

        public DateTime? CreationDate { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
    }
}
