using Reinforced.Typings.Attributes;
using Website.Enums;
using System;

namespace Website.ViewModels
{

    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "OtherConsumption")]
    public class OtherConsumptionViewModel : BaseViewModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }

        public CalculationType CalculationType { get; set; }

        public string Date { get; set; }

        public double Price { get; set; }

        public long? MansionId { get; set; }

        public string MansionName { get; set; }
    }
}