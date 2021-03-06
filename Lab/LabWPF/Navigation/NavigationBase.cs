using System;
using System.Collections.Generic;
using System.Linq;
using LI.CSharp.Lab.GUI.WPF.Checking;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF.Navigation
{
    public abstract class NavigationBase<TObject> : BindableBase where TObject : Enum
    {
        protected List<INavigatable<TObject>> _viewModels = new();

        public INavigatable<TObject> CurrentViewModel
        {
            get;
            private set;
        }

        protected NavigationBase()
        {
        }

        protected void Navigate(TObject type, AllServices allServices = null)
        {
            if (CurrentViewModel!=null && CurrentViewModel.Type.Equals(type))
                return;
            INavigatable<TObject> viewModel = _viewModels
                    .FirstOrDefault(someNavigatable => someNavigatable.Type.Equals(type));

            if (viewModel == null)
            {
                viewModel = CreateViewModel(type, allServices);
                _viewModels.Add(viewModel);
            }
            viewModel.ClearSensitiveData();
            CurrentViewModel = viewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        protected abstract INavigatable<TObject> CreateViewModel(TObject type, AllServices allServices = null);
    }
}