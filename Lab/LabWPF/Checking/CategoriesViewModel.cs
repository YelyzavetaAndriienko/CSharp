using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;
using Prism.Commands;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class CategoriesViewModel : BindableBase, INavigatable<CheckNavigatableTypes>
    {
        private CategoryService _service;
        private CategoryDetailsViewModel _currentCategory;
        private Action _gotoWallets;
        public ObservableCollection<CategoryDetailsViewModel> Categories { get; set; }

        public CategoryDetailsViewModel CurrentCategory
        {
            get
            {
                return _currentCategory;
            }
            set
            {
                _currentCategory = value;
                RaisePropertyChanged();
            }
        }

        public CategoriesViewModel(Action gotoWallets)
        {
            _service = new CategoryService();
            Categories = new ObservableCollection<CategoryDetailsViewModel>();
            foreach (var category in _service.GetCategories())
            {
                Categories.Add(new CategoryDetailsViewModel(category));
            }
            _gotoWallets = gotoWallets;
            WalletsCommand = new DelegateCommand(_gotoWallets);
        }

        public CheckNavigatableTypes Type
        {
            get
            {
                return CheckNavigatableTypes.ShowCategories;
            }
        }

        public void ClearSensitiveData()
        {

        }

        public DelegateCommand WalletsCommand { get; }

    }
}