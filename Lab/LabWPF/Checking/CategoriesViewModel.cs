using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class CategoriesViewModel : BindableBase, INavigatable<CheckNavigatableTypes>
    {
        private CategoryService _service { get; }
        private CategoryDetailsViewModel _currentCategory;
        private Action _gotoWallets;
        public ObservableCollection<CategoryDetailsViewModel> _categories;
        private Category category;

        public ObservableCollection<CategoryDetailsViewModel> Categories
        {
            get
            {
                return _categories;
            }
            private set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }


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
                //OnPropertyChanged();
            }
        }

        private async void WaitForCategoriesAsync()
        {
            if (!_service.CategoriesLoaded)
            {
                await _service.GetCategoriesAsync();
                _service.CategoriesLoaded = true;
            }
            var ws = new ObservableCollection<CategoryDetailsViewModel>();
            foreach (var category in _service.Categories)
            {
                ws.Add(new CategoryDetailsViewModel(category, this));
            }
            Categories = ws;
        }

        public CategoriesViewModel(Action gotoWallets, CategoryService service)
        {
            _service = service;
            WaitForCategoriesAsync();
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

        public void CreateCategory()
        {
            category = new Category(_service.User);
            var goodName = false;
            while (!goodName)
            {
                try
                {
                    category.Name = "new_category" + _service.User.CategoryNextNumber;
                    goodName = true;
                }
                catch (ArgumentException e) { }
            }
            if (IsCategoryEnabled())
            {
                _service.Categories.Add(category);
                _service.User.Categories.Add(category);
                CategoryDetailsViewModel wdvm = new CategoryDetailsViewModel(category, this);
                Categories.Add(wdvm);
                CurrentCategory = wdvm;
            }
            else
            {
                MessageBox.Show("Please enter name of the category (more than 2 characters)!");
            }
        }

        public void DeleteCategory()
        {
            _service.Categories.Remove(CurrentCategory.Category);
            _service.User.Categories.Remove(CurrentCategory.Category);
            Categories.Remove(CurrentCategory);
            CurrentCategory = null;
        }

        public DelegateCommand WalletsCommand { get; }

        private bool IsCategoryEnabled()
        {
            return !String.IsNullOrWhiteSpace(category.Name) && (category.Name.Length >= 2);
        }

    }
}