using System;
using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Categories;
using Prism.Mvvm;
using System.IO;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class CategoryDetailsViewModel : BindableBase
    {
        private Category _category;
        private CategoriesViewModel _wvm;

        public Category Category
        {
            get
            {
                return _category;
            }
        }

        public string Name
        {
            get
            {
                return _category.Name;
            }
            set
            {
                try
                {
                    _category.Name = value;
                    RaisePropertyChanged(nameof(DisplayName));
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public string Description
        {
            get
            {
                return _category.Description;
            }
            set
            {
                _category.Description = value;
            }
        }

        public string Color
        {
            get
            {
                return _category.Color.ToString();
            }
            set
            {
                _category.Color = (Colors?) Array.IndexOf(CategoryDetailsView.COLORS, value);
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public FileInfo Icon
        {
            get
            {
                return _category.Icon;
            }
            set
            {
                _category.Icon = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{_category.Name} {_category.Color}";
            }
        }

        public CategoryDetailsViewModel(Category category, CategoriesViewModel wvm = null)
        {
            _category = category;
            _wvm = wvm;
        }

        private bool IsCategoryEnabled()
        {
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Icon.ToString()) &&
                (Name.Length >= 2);
        }

        public void DeleteCategory()
        {
            _wvm.DeleteCategory();
        }
    }
}
