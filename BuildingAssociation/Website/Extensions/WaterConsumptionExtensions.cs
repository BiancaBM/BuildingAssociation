using Repositories.Entities;
using System;
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
                HotWaterUnits = item.HotWaterUnits,
                CreationDate = item.CreationDate.Value.ToString("MM/dd/yyyy  HH:mm"),
                UserName = item.User.Name,
                UserId = item.User.UniqueId,
                MansionId = item.User.Mansion.UniqueId,
                MansionName = item.User.Mansion.Address
            };
        }

        public static WaterConsumption FromViewModel(this WaterConsumptionViewModel viewModel)
        {
            return new WaterConsumption
            {
                ColdWaterUnits = viewModel.ColdWaterUnits,
                HotWaterUnits = viewModel.HotWaterUnits,
                UniqueId = viewModel.Id,
                CreationDate = Convert.ToDateTime(viewModel.CreationDate),
                UserId = viewModel.UserId
            };
        }
    }
}