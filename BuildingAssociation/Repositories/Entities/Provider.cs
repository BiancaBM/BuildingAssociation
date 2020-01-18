using System;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Entities
{
    public class Provider: BaseEntity
    {
        public double UnitPrice { get; set; }

        public string Name { get; set; }

        public string CUI { get; set; }

        public string BankAccount { get; set; }
    }
}
