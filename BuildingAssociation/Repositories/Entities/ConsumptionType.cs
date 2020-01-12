using Repositories.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities
{
    public class ConsumptionType : BaseEntity
    {
        public string Name { get; set; }

        public DateTime? CreationDate { get; set; }

        public CalculationType CalculationType { get; set; }
    }
}
