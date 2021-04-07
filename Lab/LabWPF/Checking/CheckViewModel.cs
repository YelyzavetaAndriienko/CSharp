using System;
using System.Collections.Generic;
using System.Linq;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class CheckViewModel : NavigationBase<CheckNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {

        public CheckViewModel(AllServices allServices)
        {
            Navigate(CheckNavigatableTypes.ShowWallets, allServices);
        }

        protected override INavigatable<CheckNavigatableTypes> CreateViewModel(CheckNavigatableTypes type, AllServices allServices)
        {
            if (type == CheckNavigatableTypes.ShowWallets)
            {
                return new WalletsViewModel(() => Navigate(CheckNavigatableTypes.ShowCategories, allServices), allServices.WalletService);
            }
            else
            {
                return new CategoriesViewModel(() => Navigate(CheckNavigatableTypes.ShowWallets, allServices), allServices.CategoryService);
            }
        }

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Check;
            }
        }

        public void ClearSensitiveData()
        {
            CurrentViewModel.ClearSensitiveData();
        }
    }
}
