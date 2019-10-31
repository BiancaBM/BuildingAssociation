using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities
{
    public class User
    {
        [Key]
        public long? UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int MembersCount { get; set; }

        public DateTime? DateCreated { get; set; }
        public Guid Guid { get; set; }

        public ICollection<Consumption> Consumptions { get; set; }
        public ICollection<Apartment> Apartments { get; set; }
    }
}
