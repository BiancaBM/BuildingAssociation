namespace Website.ViewModels
{
    public class ProviderBillViewModel
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
    }
}