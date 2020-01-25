using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class Mansion : BaseEntity
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public double? TotalFunds { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ProviderBill> Bills { get; set; }
        public virtual ICollection<OtherConsumption> Consumptions { get; set; }
    }
}