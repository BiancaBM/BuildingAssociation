using Reinforced.Typings.Attributes;
using Website.Enums;

namespace Website.ViewModels
{
    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "Provider")]
    public class ProviderViewModel : BaseViewModel
    {
        public long? ProviderId { get; set; }
        public double UnitPrice { get; set; }
        public string Name { get; set; }
        public string CUI { get; set; }
        public string BankAccount { get; set; }
        public ProviderType Type { get; set; }
    }
}