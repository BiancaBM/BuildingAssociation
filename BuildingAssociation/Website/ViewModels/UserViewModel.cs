using Reinforced.Typings.Attributes;
using System.Collections.Generic;

namespace Website.ViewModels
{
    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "User")]
    public class UserViewModel : BaseViewModel
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MembersCount { get; set; }
        public long? MansionId { get; set; }
        public string MansionName { get; set; }
        public ICollection<WaterConsumptionViewModel> WaterConsumptions { get; set; }
        public ICollection<ApartmentViewModel> Apartments { get; set; }
    }
}