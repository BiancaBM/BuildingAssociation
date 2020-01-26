using Repositories.Entities;
using System.Linq;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class BillGeneratorExtensions
    {
        public static BillGeneratorViewModel ToViewModel(this GeneratedBill item)
        {
            return new BillGeneratorViewModel
            {
                Id = item.UniqueId,
                MansionId = item.MansionId,
                MansionName = item.Mansion.Address,
                CSV = item.CSV,
                Date = item.Date.ToString("MMMM-yyyy")
            };
        }
    }
}