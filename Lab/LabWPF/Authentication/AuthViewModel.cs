using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF.Authentication
{
    public class AuthViewModel : NavigationBase<AuthNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {
        private Action _signInSuccess;
        private User _user;

        public User User
        {
            get
            {
                return _user;
            }
        }
        
        public AuthViewModel(Action signInSuccess)
        {
            _signInSuccess = signInSuccess;
            Navigate(AuthNavigatableTypes.SignIn);
            _user = ((SignInViewModel) CurrentViewModel).User;
            //MessageBox.Show(_user.ToString());
        }
        
        protected override INavigatable<AuthNavigatableTypes> CreateViewModel(AuthNavigatableTypes type, Object obj)
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
