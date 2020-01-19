using Reinforced.Typings.Attributes;

namespace Website.ViewModels
{
    [TsInterface(AutoI = false, IncludeNamespace = false, Name ="Apartment")]
    public class ApartmentViewModel : BaseViewModel
    {
        public long? ApartmentId { get; set; }
        public int Surface { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
    }
}