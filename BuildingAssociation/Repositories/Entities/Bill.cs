using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class Bill
    {
        [Key]
        public long? BillId { get; set; }

        [ForeignKey("Provider")]
        public long? ProviderId { get; set; }
        public Provider Provider { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public bool Paid { get; set; }

        public DateTime? CreationDate { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
        public Guid Guid { get; set; }

    }
}
