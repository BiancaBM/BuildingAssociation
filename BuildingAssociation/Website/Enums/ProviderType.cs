using Reinforced.Typings.Attributes;

namespace Website.Enums
{
    [TsEnum(IncludeNamespace = false, Name = "ProviderType")]
    public enum ProviderType
    {
        Electricity,
        Water,
        Other
    }
}
