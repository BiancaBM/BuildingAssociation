using Repositories.Entities;
using System.Linq;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class MansionExtensions
    {
        public static MansionViewModel ToViewModel(this Mansion item)
        {
            return new MansionViewModel
            {
                Id = item.UniqueId,
                Address = item.Address,
                TotalFunds = item.TotalFunds,
                Users = item.Users.Select(x => x.ToViewModel()).ToList(),
                Bills = item.Bills.Select(x => x.ToViewModel()).ToList()
            };
        }

        public static Mansion FromViewModel(this MansionViewModel viewModel)
        {
            return new Mansion
            {
                Address = viewModel.Address,
                TotalFunds = viewModel.TotalFunds,
                UniqueId = viewModel.Id
            };
        }
    }
}