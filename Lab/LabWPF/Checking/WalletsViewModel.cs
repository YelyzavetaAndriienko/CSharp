using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;
using Prism.Commands;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class WalletsViewModel : NavigationBase<CheckNavigatableTypes>, INavigatable<CheckNavigatableTypes>
    {
        private WalletService _service { get; }
        private WalletDetailsViewModel _currentWallet;
        private Action _gotoCategories;
        public ObservableCollection<WalletDetailsViewModel> _wallets;
        private Wallet wallet;

        public ObservableCollection<WalletDetailsViewModel> Wallets
        {
            get
            {
                return _wallets;
            }
            private set
            {
                _wallets = value;
                RaisePropertyChanged();
                //OnPropertyChanged();
            }
        }

        public WalletDetailsViewModel CurrentWallet
        {
            get
            {
                return _currentWallet;
            }
            set
            {
                _currentWallet = value;
                //RaisePropertyChanged(nameof(_currentWallet.MainCurrency));
                RaisePropertyChanged();
                //OnPropertyChanged();
            }
        }

        private async void WaitForWalletsAsync()
        {
            if (!_service.WalletsLoaded)
            {
                await _service.GetWalletsAsync();
                _service.WalletsLoaded = true;
            }
            var ws = new ObservableCollection<WalletDetailsViewModel>();
            foreach (var wallet in _service.Wallets)
            {
                ws.Add(new WalletDetailsViewModel(wallet, () => Navigate(CheckNavigatableTypes.ShowTransactions), this));
            }
            Wallets = ws;
        }

        public WalletsViewModel(Action gotoCategories, WalletService service)
        {
            _service = service;
            WaitForWalletsAsync();
            _gotoCategories = gotoCategories;
            CategoriesCommand = new DelegateCommand(_gotoCategories);
        }
        
        //public DelegateCommand AddWallet { get; }

        public CheckNavigatableTypes Type
        {
            get
            {
                return CheckNavigatableTypes.ShowWallets;
            }
        }

        public void ClearSensitiveData()
        {
            
        }

        public void CreateWallet()
        {
            wallet = new Wallet(_service.User);
            var goodName = false;
            while (!goodName)
            {
                try  
                { 
                    wallet.Name = "new_wallet" + _service.User.WalletNextNumber;
                    goodName = true;
                }
                catch (ArgumentException e) { }
            }
            if (IsWalletEnabled())
            {
            _service.Wallets.Add(wallet);
            _service.User.MyWallets.Add(wallet);
            WalletDetailsViewModel wdvm = new WalletDetailsViewModel(wallet, () => Navigate(CheckNavigatableTypes.ShowTransactions), this);
            Wallets.Add(wdvm);
            CurrentWallet = wdvm;
            }
            else
            {
                MessageBox.Show("Please enter name of the category (more than 2 characters)!");
            }
        }
        
        public void DeleteWallet()
        {
            _service.Wallets.Remove(CurrentWallet.Wallet);
            _service.User.MyWallets.Remove(CurrentWallet.Wallet);
            Wallets.Remove(CurrentWallet);
            CurrentWallet = null;
        }
        public DelegateCommand CategoriesCommand { get; }

        private bool IsWalletEnabled()
        {
            return !String.IsNullOrWhiteSpace(wallet.Name) && !String.IsNullOrWhiteSpace(wallet.InitialBalance.ToString()) &&
                   !String.IsNullOrWhiteSpace(wallet.CurrentBalance.ToString()) && (wallet.Name.Length >= 2);
        }

        protected override INavigatable<CheckNavigatableTypes> CreateViewModel(CheckNavigatableTypes type, AllServices allServices = null)
        {
            throw new NotImplementedException();
        }
    }
}