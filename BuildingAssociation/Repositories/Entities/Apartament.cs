﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class Apartment : BaseEntity
    {
        [Required]
        public int Surface { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int Floor { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }
        public User User { get; set; }
    }
}