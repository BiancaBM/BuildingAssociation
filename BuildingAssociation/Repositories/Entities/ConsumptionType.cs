using Repositories.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class ConsumptionType : BaseEntity
    {
        public string Name { get; set; }

        public CalculationType CalculationType { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("Mansion")]
        public long? MansionId { get; set; }
        public virtual Mansion Mansion { get; set; }
    }
}
