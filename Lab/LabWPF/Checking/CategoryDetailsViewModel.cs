using LI.CSharp.Lab.Models.Categories;//
using Prism.Mvvm;
using System.IO;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class CategoryDetailsViewModel : BindableBase
    {
        private Category _category;

        public string Name
        {
            get
            {
                return _category.Name;
            }
            set
            {
                _category.Name = value;
                RaisePropertyChanged(nameof(DisplayName));
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
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string Color
        {
            get
            {
                return _category.Color;
            }
            set
            {
                _category.Color = value;
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
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{_category.Name} (${_category.Color})";
            }
        }

        public CategoryDetailsViewModel(Category category)
        {
            _category = category;
        }
    }
}
