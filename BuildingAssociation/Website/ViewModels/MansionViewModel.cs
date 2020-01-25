using Reinforced.Typings.Attributes;
using System.Collections.Generic;

namespace Website.ViewModels
{

    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "Mansion")]
    public class MansionViewModel : BaseViewModel
    {
        public long? Id { get; set; }
        public string Address { get; set; }
        public double? TotalFunds { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<ProviderBillViewModel> Bills { get; set; }
        public IEnumerable<OtherConsumptionViewModel> Consumptions { get; set; }
    }
}