using System;
using System.Collections.Generic;
using System.Linq;
using LI.CSharp.Lab.GUI.WPF.Authentication;
using LI.CSharp.Lab.GUI.WPF.Checking;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF
{
    public class MainViewModel : NavigationBase<MainNavigatableTypes>
    {
        public MainViewModel()
        {
            Navigate(MainNavigatableTypes.Auth);
        }
        
        protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type)
        {
            if (type == MainNavigatableTypes.Auth)
            {
                return new AuthViewModel(() => Navigate(MainNavigatableTypes.Check));
            }
            else
            {
                return new CheckViewModel();
            }
        }
    }
}