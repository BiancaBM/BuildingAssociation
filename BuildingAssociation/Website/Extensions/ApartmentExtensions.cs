using Repositories.Entities;
using System;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class ApartmentExtensions
    {
        public static ApartmentViewModel ToViewModel(this Apartment item)
        {
            return new ApartmentViewModel
            {
                ApartmentId = item.UniqueId,
                Floor = item.Floor,
                Number = item.Number,
                Surface = item.Surface,
                IndividualQuota = item.IndividualQuota,
                MansionId = item.Mansion.UniqueId,
                MansionName = item.Mansion.Address,
                UserId = item.User.UniqueId,
                UserName = item.User.Name,
                MembersCount = item.MembersCount
            };
        }

        public static Apartment FromViewModel(this ApartmentViewModel viewModel)
        {
            return new Apartment
            {
                UniqueId = viewModel.ApartmentId,
                Surface = Math.Round(viewModel.Surface, 2),
                Number = viewModel.Number,
                Floor = viewModel.Floor,
                IndividualQuota = viewModel.IndividualQuota,
                UserId = viewModel.UserId,
                MansionId = viewModel.MansionId,
                MembersCount = viewModel.MembersCount
            };
        }
    }
}