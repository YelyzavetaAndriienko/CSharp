using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;
using Prism.Commands;

namespace LI.CSharp.Lab.GUI.WPF.Wallets
{
    public class WalletsViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        private WalletService _service { get; }
        private WalletDetailsViewModel _currentWallet;
        public ObservableCollection<WalletDetailsViewModel> Wallets { get; }

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

        public WalletsViewModel()
        {
            _service = new WalletService();
            Wallets = new ObservableCollection<WalletDetailsViewModel>();
            foreach (var wallet in _service.GetWallets())
            {
                Wallets.Add(new WalletDetailsViewModel(wallet, this));
            }
        }
        
        //public DelegateCommand AddWallet { get; }

        public MainNavigatableTypes Type 
        {
            get
            {
                return MainNavigatableTypes.Wallets;
            }
        }

        public void ClearSensitiveData()
        {
            
        }

        public void CreateWallet()
        {
            Wallet wallet = new Wallet(WalletService.u);
            wallet.Name = "new_wallet" + WalletService.u.WalletNextNumber;
            _service.GetWallets().Add(wallet);
            WalletDetailsViewModel wdvm = new WalletDetailsViewModel(wallet, this);
            Wallets.Add(wdvm);
            CurrentWallet = wdvm;
        }
        
        public void DeleteWallet()
        {
            _service.GetWallets().Remove(CurrentWallet.Wallet);
            Wallets.Remove(CurrentWallet);
            CurrentWallet = null;
        }
    }
}