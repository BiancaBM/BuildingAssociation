using Repositories.Entities;
using System;
using Website.ViewModels;

namespace Website.Extensions
{
    public static class ProviderBillExtensions
    {
        public static ProviderBillViewModel ToViewModel(this ProviderBill bill)
        {
            return new ProviderBillViewModel
            {
                BillId = bill.UniqueId,
                ProviderId = bill.Provider.UniqueId,
                ProviderName = bill.Provider.Name,
                DueDate = bill.DueDate.Value.ToString("MM/dd/yyyy"),
                Other = Math.Round(bill.Other, 2),
                Units = Math.Round(bill.Units, 2),
                Paid = bill.Paid,
                ProviderUnitPrice = bill.ProviderUnitPrice,
                TotalPrice = Math.Round((bill.Units * bill.ProviderUnitPrice + bill.Other).Value, 2),
            };
        }

        public static ProviderBill FromViewModel(this ProviderBillViewModel viewModel)
        {
            return new ProviderBill
            {
                UniqueId = viewModel.BillId,
                ProviderId = viewModel.ProviderId,
                Other = viewModel.Other,
                Units = viewModel.Units,
                ProviderUnitPrice = viewModel.ProviderUnitPrice,
                Paid = viewModel.Paid,
                DueDate = Convert.ToDateTime(viewModel.DueDate)
            };
        }
    }
}