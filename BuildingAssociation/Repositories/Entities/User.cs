using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Roles { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime? DateCreated { get; set; }

        [ForeignKey("Mansion")]
        public long? MansionId { get; set; }
        public virtual Mansion Mansion { get; set; }

        public virtual ICollection<WaterConsumption> WaterConsumptions { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
