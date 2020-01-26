using Reinforced.Typings.Attributes;
using System.Collections.Generic;
using Website.Enums;

namespace Website.ViewModels
{
    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "BillGenerator")]
    public class BillGeneratorViewModel : BaseViewModel
    {
        public long? Id { get; set; }
        public long? MansionId { get; set; }
        public string MansionName { get; set; }
        public string CSV { get; set; }
        public string Date { get; set; }
    }
}