using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF.Authentication
{
    public class AuthViewModel : BindableBase, IMainNavigatable
    {
        private List<IAuthNavigatable> _viewModels = new List<IAuthNavigatable>();
        private Action _signInSuccess;

        public IAuthNavigatable CurrentViewModel
        {
            get;
            private set;
        }
        public AuthViewModel(Action signInSuccess)
        {
            _signInSuccess = signInSuccess;
            Navigate(AuthNavigatableTypes.SignIn);
        }

        public void Navigate(AuthNavigatableTypes type)
        {
            if (CurrentViewModel != null && CurrentViewModel.Type == type)
                return;
            IAuthNavigatable viewModel = _viewModels.FirstOrDefault(authNavigatable => authNavigatable.Type == type);

            if (viewModel == null)
            {
                viewModel = CreateViewModel(type);
                _viewModels.Add(viewModel);
            }
            viewModel.ClearSensitiveData();
            CurrentViewModel = viewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        private IAuthNavigatable CreateViewModel(AuthNavigatableTypes type)
        {
            if (type == AuthNavigatableTypes.SignIn)
            {
                return new SignInViewModel(() => Navigate(AuthNavigatableTypes.SignUp), _signInSuccess);
            }
            else
            {
                return new SignUpViewModel(() => Navigate(AuthNavigatableTypes.SignIn));
            }
        }

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Auth;
            }
        }

        public void ClearSensitiveData()
        {
            CurrentViewModel.ClearSensitiveData();
        }
    }
}
