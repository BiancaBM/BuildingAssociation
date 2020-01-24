using Reinforced.Typings.Attributes;

namespace Website.ViewModels
{
    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "ProviderBill")]
    public class ProviderBillViewModel : BaseViewModel
    {
        public long? BillId { get; set; }
        public string ProviderName { get; set; }
        public long? ProviderId { get; set; } 
        public double Units { get; set; }
        public double Other { get; set; }
        public bool Paid { get; set; }
        public double? TotalPrice { get; set; }
        public double? ProviderUnitPrice { get; set; }
        public string DueDate { get; set; }
        public string Date { get; set; }
        public long? MansionId { get; set; }
        public string MansionName { get; set; }
    }
}