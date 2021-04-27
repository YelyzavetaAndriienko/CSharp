using System;
using System.Collections.ObjectModel;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;
using Prism.Commands;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class WalletsViewModel : BindableBase, INavigatable<CheckNavigatableTypes>
    {
        private WalletService _service;
        private WalletDetailsViewModel _currentWallet;
        private Action _gotoCategories;
        private ObservableCollection<WalletDetailsViewModel> _wallets;
        public CheckViewModel Cwm { get; }

        public WalletService Service
        {
            get
            {
                return _service;
            }
        }

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
                _service.SetCurrentWalletInTransactionService(_currentWallet != null ? 
                    CurrentWallet.Wallet : null);
                RaisePropertyChanged();
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
                ws.Add(new WalletDetailsViewModel(wallet, this));
            }
            Wallets = ws;
        }

        public WalletsViewModel(Action gotoCategories, WalletService service, CheckViewModel cwm)
        {
            _service = service;
            Cwm = cwm;
            WaitForWalletsAsync();
            _gotoCategories = gotoCategories;
            CategoriesCommand = new DelegateCommand(_gotoCategories);
        }

        public CheckNavigatableTypes Type
        {
            get
            {
                return CheckNavigatableTypes.ShowWallets;
            }
        }

        public void ClearSensitiveData() { }

        public void CreateWallet()
        {
            Wallet wallet = new Wallet(_service.User);
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
            _service.Wallets.Add(wallet);
            _service.User.MyWallets.Add(wallet);
            WalletDetailsViewModel wdvm = new WalletDetailsViewModel(wallet, this);
            Wallets.Add(wdvm);
            CurrentWallet = wdvm;
        }

        public void DeleteWallet()
        {
            _service.Wallets.Remove(CurrentWallet.Wallet);
            _service.User.MyWallets.Remove(CurrentWallet.Wallet);
            Wallets.Remove(CurrentWallet);
            CurrentWallet = null;
        }
        public DelegateCommand CategoriesCommand { get; }

        /*private bool IsWalletEnabled()
        {
            return !String.IsNullOrWhiteSpace(wallet.Name) && !String.IsNullOrWhiteSpace(wallet.InitialBalance.ToString()) &&
                   !String.IsNullOrWhiteSpace(wallet.CurrentBalance.ToString()) && (wallet.Name.Length >= 2);
        }*/
        
    }
}