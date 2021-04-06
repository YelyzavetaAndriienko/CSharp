using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class WalletsViewModel : BindableBase, INavigatable<CheckNavigatableTypes>
    {
        private WalletService _service { get; }
        private WalletDetailsViewModel _currentWallet;
        private Action _gotoCategories;
        public ObservableCollection<WalletDetailsViewModel> _wallets;

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
                //RaisePropertyChanged(nameof(_currentWallet.MainCurrency));
                RaisePropertyChanged();
            }
        }

        private async void WaitForWallets()
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

        public WalletsViewModel(Action gotoCategories, WalletService service)
        {
            _service = service;
            WaitForWallets();
            /*if (!_service.WalletsLoaded)
            {
                //Thread.Sleep(1000);
                //WaitForWallets();
                //_service.GetWalletsAsync();
                //Task.Run(async () => await _service.GetWalletsAsync());
                //Thread.CurrentThread.Join();
                _service.GetWalletsAsync();
                _service.WalletsLoaded = true;
            }
            Wallets = new ObservableCollection<WalletDetailsViewModel>();
            foreach (var wallet in _service.Wallets)
            {
                Wallets.Add(new WalletDetailsViewModel(wallet, this));
            }*/
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
            Wallet wallet = new Wallet(_service.User);
            wallet.Name = "new_wallet" + _service.User.WalletNextNumber;
            _service.Wallets.Add(wallet);
            WalletDetailsViewModel wdvm = new WalletDetailsViewModel(wallet, this);
            Wallets.Add(wdvm);
            CurrentWallet = wdvm;
        }
        
        public void DeleteWallet()
        {
            _service.Wallets.Remove(CurrentWallet.Wallet);
            Wallets.Remove(CurrentWallet);
            CurrentWallet = null;
        }
        public DelegateCommand CategoriesCommand { get; }

    }
}