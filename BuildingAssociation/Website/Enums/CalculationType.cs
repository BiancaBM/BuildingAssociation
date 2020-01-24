using Reinforced.Typings.Attributes;

namespace Website.Enums
{
    [TsEnum(IncludeNamespace = false, Name = "CalculationType")]
    public enum CalculationType
    {
        NumberOfMembers,
        IndividualQuota
    }
}
