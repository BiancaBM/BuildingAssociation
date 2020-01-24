using Repositories.Entities;
using System.Linq;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class UserExtensions
    {
        public static UserViewModel ToViewModel(this User item)
        {
            return new UserViewModel
            {
                UserId = item.UniqueId,
                Email = item.Email,
                Name = item.Name,
                MembersCount = item.MembersCount,
                Apartments = item.Apartments != null ? item.Apartments.Select(x => x.ToViewModel()).ToList() : null,
                WaterConsumptions = item.WaterConsumptions != null ? item.WaterConsumptions.Select(x => x.ToViewModel()).ToList() : null,
                IsAdmin = item.Roles.Contains("Admin"),
                MansionId = item.MansionId,
                MansionName = item.Mansion != null ? item.Mansion.Address : string.Empty,
            };
        }

        public static User FromViewModel(this UserViewModel viewModel)
        {
            return new User
            {
                UniqueId = viewModel.UserId,
                Email = viewModel.Email,
                Name = viewModel.Name,
                MembersCount = viewModel.MembersCount,
                MansionId = viewModel.MansionId,
                Password = viewModel.Password
            };
        }
    }
}