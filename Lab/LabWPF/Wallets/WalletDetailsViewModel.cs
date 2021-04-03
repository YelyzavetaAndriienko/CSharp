using LI.CSharp.Lab.Models.Wallets;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF.Wallets
{
    public class WalletDetailsViewModel : BindableBase
    {
        private Wallet _wallet;

        public string Name
        {
            get
            {
                return _wallet.Name;
            }
            set
            {
                _wallet.Name = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }
        
        public string Description
        {
            get
            {
                return _wallet.Description;
            }
            set
            {
                _wallet.Description = value;
            }
        }
        
        public decimal InitialBalance
        {
            get
            {
                return _wallet.InitialBalance;
            }
            set
            {
                _wallet.InitialBalance = value;
                RaisePropertyChanged(nameof(DisplayName));
                //CurrentBalance = _wallet.CurrentBalance;
            }
        }

        public decimal CurrentBalance
        {
            get
            {
                return _wallet.CurrentBalance;
            }
        }
        
        public Currencies? MainCurrency
        {
            get
            {
                return _wallet.MainCurrency;
            }
            set
            {
                _wallet.MainCurrency = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{_wallet.Name} {_wallet.CurrentBalance} {_wallet.MainCurrency}";       
            }
        }

        public WalletDetailsViewModel(Wallet wallet)
        {
            _wallet = wallet;
        }
    }
}