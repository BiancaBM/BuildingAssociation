using Repositories.Entities;
using Repositories.Entities.Enums;
using System;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class OtherConsumptionExtensions
    {
        public static OtherConsumptionViewModel ToViewModel(this OtherConsumption item)
        {
            Enums.CalculationType calculationType;
            Enum.TryParse(item.CalculationType.ToString(), out calculationType);

            return new OtherConsumptionViewModel
            {
                CalculationType = calculationType,
                Name = item.Name,
                Id = item.UniqueId,
                Date = item.Date.Value.ToString("MM/dd/yyyy"),
                MansionId = item.Mansion.UniqueId,
                MansionName = item.Mansion.Address,
                Price = item.Price
            };
        }

        public static OtherConsumption FromViewModel(this OtherConsumptionViewModel viewModel)
        {
            CalculationType calculationType;
            Enum.TryParse(viewModel.CalculationType.ToString(), out calculationType);

            return new OtherConsumption
            {
                UniqueId = viewModel.Id,
                Name = viewModel.Name,
                CalculationType = calculationType,
                Date = Convert.ToDateTime(viewModel.Date),
                MansionId = viewModel.MansionId,
                Price = viewModel.Price
            };
        }
    }
}