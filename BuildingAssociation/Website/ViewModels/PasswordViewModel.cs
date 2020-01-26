using Reinforced.Typings.Attributes;
using System.Collections.Generic;

namespace Website.ViewModels
{
    [TsInterface(AutoI = false, IncludeNamespace = false, Name = "Password")]
    public class PasswordViewModel : BaseViewModel
    {
        public string Password { get; set; }
    }
}