using Repositories.Entities;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class WaterConsumptionExtensions
    {
        public static WaterConsumptionViewModel ToViewModel(this WaterConsumption item)
        {
            return new WaterConsumptionViewModel
            {
                Id = item.UniqueId,
                ColdWaterUnits = item.ColdWaterUnits,
                HotWaterUnits = item.HotWaterUnits
            };
        }

        public static WaterConsumption FromViewModel(this WaterConsumptionViewModel viewModel)
        {
            return new WaterConsumption
            {
                ColdWaterUnits = viewModel.ColdWaterUnits,
                HotWaterUnits = viewModel.HotWaterUnits,
                UniqueId = viewModel.Id
            };
        }
    }
}