using Repositories.Entities;
using Repositories.Entities.Enums;
using System;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class ConsumptionTypeExtensions
    {
        public static ConsumptionTypeViewModel ToViewModel(this ConsumptionType item)
        {
            Enums.CalculationType calculationType;
            Enum.TryParse(item.CalculationType.ToString(), out calculationType);

            return new ConsumptionTypeViewModel
            {
                CalculationType = calculationType,
                Name = item.Name,
                Id = item.UniqueId,
                Date = item.Date.Value.ToString("MM/dd/yyyy"),
                MansionId = item.Mansion.UniqueId,
                MansionName = item.Mansion.Address
            };
        }

        public static ConsumptionType FromViewModel(this ConsumptionTypeViewModel viewModel)
        {
            CalculationType calculationType;
            Enum.TryParse(viewModel.CalculationType.ToString(), out calculationType);

            return new ConsumptionType
            {
                UniqueId = viewModel.Id,
                Name = viewModel.Name,
                CalculationType = calculationType,
                Date = Convert.ToDateTime(viewModel.Date),
                MansionId = viewModel.MansionId
            };
        }
    }
}