using System;
using System.Collections.Generic;
using System.Linq;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class CheckViewModel : NavigationBase<CheckNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {

        public CheckViewModel()
        {
            Navigate(CheckNavigatableTypes.ShowWallets);
        }

        protected override INavigatable<CheckNavigatableTypes> CreateViewModel(CheckNavigatableTypes type)
        {
            if (type == CheckNavigatableTypes.ShowWallets)
            {
                return new WalletsViewModel(() => Navigate(CheckNavigatableTypes.ShowCategories));
            }
            else
            {
                return new CategoriesViewModel(() => Navigate(CheckNavigatableTypes.ShowWallets));
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
