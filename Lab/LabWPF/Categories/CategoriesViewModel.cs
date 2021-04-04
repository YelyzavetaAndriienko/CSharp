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

namespace LI.CSharp.Lab.GUI.WPF.Categories
{
    public class CategoriesViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        private CategoryService _service;
        private CategoryDetailsViewModel _currentCategory;
        private Action _showCategories;
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

        public CategoriesViewModel(Action showCategories)
        {
            _service = new CategoryService();
            Categories = new ObservableCollection<CategoryDetailsViewModel>();
            foreach (var category in _service.GetCategories())
            {
                Categories.Add(new CategoryDetailsViewModel(category));
            }
        }

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Categories;
            }
        }

        public void ClearSensitiveData()
        {

        }

    }
}