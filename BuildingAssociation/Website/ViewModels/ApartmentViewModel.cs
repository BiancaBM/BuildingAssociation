using Reinforced.Typings.Attributes;

namespace Website.ViewModels
{
    [TsInterface(AutoI = false, IncludeNamespace = false, Name ="Apartment")]
    public class ApartmentViewModel : BaseViewModel
    {
        public long? ApartmentId { get; set; }
        public double Surface { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public double IndividualQuota { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public long? MansionId { get; set; }
        public string MansionName { get; set; }
        public int MembersCount { get; set; }
    }
}