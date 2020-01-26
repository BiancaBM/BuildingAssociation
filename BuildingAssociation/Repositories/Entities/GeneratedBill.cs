using Repositories.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class GeneratedBill : BaseEntity
    {
        public string CSV { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Mansion")]
        public long? MansionId { get; set; }
        public virtual Mansion Mansion { get; set; }
    }
}
