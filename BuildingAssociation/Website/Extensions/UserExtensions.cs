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
                Apartments = item.Apartments.Select(x => x.ToViewModel()).ToList(),
                WaterConsumptions = item.WaterConsumptions.Select(x => x.ToViewModel()).ToList(),
                IsAdmin = item.Roles.IndexOf("Admin") > 0
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
            };
        }
    }
}