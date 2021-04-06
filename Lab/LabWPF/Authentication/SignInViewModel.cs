using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Services;
using Prism.Commands;

namespace LI.CSharp.Lab.GUI.WPF.Authentication
{
    public class SignInViewModel : INotifyPropertyChanged, INavigatable<AuthNavigatableTypes>
    {
        private AuthenticationUser _authUser = new AuthenticationUser();
        private Action _gotoSignUp;
        private Action _gotoChecking;
        private bool _isEnabled = true;
        private User _user;

        public User User
        {
            get
            {
                return _user;
            }
        }
        
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public AuthNavigatableTypes Type
        {
            get
            {
                return AuthNavigatableTypes.SignIn;
            }
        }
        public string Login
        {
            get
            {
                return _authUser.Login;
            }
            set
            {
                if (_authUser.Login != value)
                {
                    _authUser.Login = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string Password
        {
            get
            {
                return _authUser.Password;
            }
            set
            {
                if (_authUser.Password != value)
                {
                    _authUser.Password = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand SignInCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public SignInViewModel(Action gotoSignUp, Action gotoChecking)
        {
            _user = new User();
            SignInCommand = new DelegateCommand(SignIn, IsSignInEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            _gotoSignUp = gotoSignUp;
            SignUpCommand = new DelegateCommand(_gotoSignUp);
            _gotoChecking = gotoChecking;
        }

        private async void SignIn()
        {
            if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password))
                MessageBox.Show("Login or password is empty.");
            else
            {
                var authService = new AuthenticationService();
                try
                {
                    IsEnabled = false;
                    //_user = await authService.AuthenticateAsync(_authUser);
                    _user.Copy(await authService.AuthenticateAsync(_authUser));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed: {ex.Message}");
                    return;
                }
                finally
                {
                    IsEnabled = true;
                }
                MessageBox.Show($"Sign In was successful for user {_user.Name} {_user.Surname}");
                _gotoChecking.Invoke();
            }
        }

        private bool IsSignInEnabled()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
                                                    // && (Login.Length > 2) && (Password.Length > 3);
        }

        public void ClearSensitiveData()
        {
            Password = "";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
