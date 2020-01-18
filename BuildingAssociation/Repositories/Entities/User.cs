using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public int MembersCount { get; set; }

        public DateTime? DateCreated { get; set; }

        public ICollection<WaterConsumption> WaterConsumptions { get; set; }
        public ICollection<Apartment> Apartments { get; set; }
    }
}
