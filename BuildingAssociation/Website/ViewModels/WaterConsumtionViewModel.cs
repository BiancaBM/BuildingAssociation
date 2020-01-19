using Reinforced.Typings.Attributes;

namespace Website.ViewModels
{

    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "WaterConsumption")]
    public class WaterConsumptionViewModel : BaseViewModel
    {
        public long? Id { get; set; }
        public double HotWaterUnits { get; set; }
        public double ColdWaterUnits { get; set; }
    }
}