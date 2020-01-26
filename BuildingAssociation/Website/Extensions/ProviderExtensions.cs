using Repositories.Entities;
using System;
using Website.Enums;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class ProviderExtensions
    {
        public static ProviderViewModel ToViewModel(this Provider item)
        {
            ProviderType type;
            Enum.TryParse(item.Type.ToString(), out type);

            return new ProviderViewModel
            {
                BankAccount = item.BankAccount,
                CUI = item.CUI,
                Name = item.Name,
                ProviderId = item.UniqueId,
                UnitPrice = item.UnitPrice,
                Type = type
            };
        }

        public static Provider FromViewModel(this ProviderViewModel viewModel)
        {
            Repositories.Entities.Enums.ProviderType type;
            Enum.TryParse(viewModel.Type.ToString(), out type);

            return new Provider
            {
                UnitPrice = viewModel.UnitPrice,
                UniqueId = viewModel.ProviderId,
                Name = viewModel.Name,
                CUI = viewModel.CUI,
                BankAccount = viewModel.BankAccount,
                Type = type
            };
        }
    }
}