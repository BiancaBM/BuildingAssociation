using Reinforced.Typings.Attributes;
using System;

namespace Website.ViewModels
{

    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "WaterConsumption")]
    public class WaterConsumptionViewModel : BaseViewModel
    {
        public long? Id { get; set; }
        public double KitchenUnits { get; set; }
        public double BathroomUnits { get; set; }
        public string CreationDate { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public long? MansionId { get; set; }
        public string MansionName { get; set; }
    }
}