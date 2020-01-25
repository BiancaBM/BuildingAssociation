using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class UserBillItem : BaseEntity
    {
        [ForeignKey("User")]
        public long? UserId { get; set; }
        public User User { get; set; }

        [Required]
        public OtherConsumption ConsumptionType { get; set; }

        [Required]
        public double Price { get; set; }

        public DateTime? CreationDate { get; set; }

    }
}
