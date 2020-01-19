using Repositories.Entities;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class ProviderExtensions
    {
        public static ProviderViewModel ToViewModel(this Provider item)
        {
            return new ProviderViewModel
            {
                BankAccount = item.BankAccount,
                CUI = item.CUI,
                Name = item.Name,
                ProviderId = item.UniqueId,
                UnitPrice = item.UnitPrice
            };
        }

        public static Provider FromViewModel(this ProviderViewModel viewModel)
        {
            return new Provider
            {
                UnitPrice = viewModel.UnitPrice,
                UniqueId = viewModel.ProviderId,
                Name = viewModel.Name,
                CUI = viewModel.CUI,
                BankAccount = viewModel.BankAccount
            };
        }
    }
}