using System;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities
{
    public class Provider
    {
        [Key]
        public long? ProviderId { get; set; }

        public double UnitPrice { get; set; }

        public string Name { get; set; }

        public string CUI { get; set; }

        public string BankAccount { get; set; }

        public Guid Guid { get; set; }
    }
}
