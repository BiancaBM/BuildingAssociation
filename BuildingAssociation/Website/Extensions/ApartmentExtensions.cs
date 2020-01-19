using Repositories.Entities;
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
                Surface = item.Surface
            };
        }

        public static Apartment FromViewModel(this ApartmentViewModel viewModel)
        {
            return new Apartment
            {
                UniqueId = viewModel.ApartmentId,
                Surface = viewModel.Surface,
                Number = viewModel.Number,
                Floor = viewModel.Floor
            };
        }
    }
}