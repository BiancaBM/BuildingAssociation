using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class Apartment : BaseEntity
    {
        [Required]
        public double Surface { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public double IndividualQuota { get; set; }

        [Required]
        public int MembersCount { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Mansion")]
        public long? MansionId { get; set; }
        public virtual Mansion Mansion { get; set; }
    }
}