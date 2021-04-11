using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Wallets;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;
using Prism.Commands;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class WalletDetailsViewModel : NavigationBase<CheckNavigatableTypes>
    {
        private Wallet _wallet;
        private WalletsViewModel _wvm;
        private Action _gotoTransactions;

        public Wallet Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public string Name
        {
            get
            {
                return _wallet.Name;
            }
            set
            {
                try
                {
                    _wallet.Name = value;
                    RaisePropertyChanged(nameof(DisplayName));
                    //if (String.IsNullOrWhiteSpace(Name) && (Name.Length < 2))
                    //{
                    //    MessageBox.Show("Please enter name of the wallet (more than 2 characters)!");
                    //}
                    //else
                    //{
                    //    RaisePropertyChanged(nameof(DisplayName));
                    //}
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message);
                }
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
                return Math.Round(_wallet.InitialBalance, 2);
            }
            set
            {
                _wallet.InitialBalance = value;
                RaisePropertyChanged(nameof(DisplayName));
                RaisePropertyChanged(nameof(CurrentBalance));
                //if (!String.IsNullOrWhiteSpace(_wallet.InitialBalance.ToString()))
                //{
                //    MessageBox.Show("Please enter initial balance!");
                //}
                //else
                //{
                //    RaisePropertyChanged(nameof(DisplayName));
                //    RaisePropertyChanged(nameof(CurrentBalance));
                //}
            }
        }

        public decimal CurrentBalance
        {
            get
            {
                return Math.Round(_wallet.CurrentBalance, 2);
            }
        }

        public string MainCurrency
        {
            get
            {
                return _wallet.MainCurrency.ToString();
            }
            set
            {
                _wallet.MainCurrency = (Currencies?)Array.IndexOf(WalletDetailsView.CURRENCIES, value);
                RaisePropertyChanged(nameof(DisplayName));
                RaisePropertyChanged(nameof(CurrentBalance));
                RaisePropertyChanged(nameof(InitialBalance));
            }
        }

        public decimal GeneralSumOfIncomeForMonth
        {
            get
            {
                return Math.Round(_wallet.GeneralSumOfIncomeForMonth(), 2);
            }
        }

        public decimal GeneralSumOfSpendingForMonth
        {
            get
            {
                return Math.Round(_wallet.GeneralSumOfSpendingsForMonth(), 2);
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{_wallet.Name} {Math.Round(_wallet.CurrentBalance, 2)} {_wallet.MainCurrency}";
            }
        }

        public WalletDetailsViewModel(Wallet wallet, WalletsViewModel wvm = null)
        {
            //if (IsWalletEnabled())
            //{
            _wallet = wallet;
            _wvm = wvm;
            _gotoTransactions = () => Navigate(CheckNavigatableTypes.ShowTransactions);
            TransactionsCommand = new DelegateCommand(_gotoTransactions);
            // }
        }

        private bool IsWalletEnabled()
        {
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(InitialBalance.ToString()) &&
                   !String.IsNullOrWhiteSpace(CurrentBalance.ToString()) && (Name.Length >= 2);
        }

        public void DeleteWallet()
        {
            _wvm.DeleteWallet();
        }

        public DelegateCommand TransactionsCommand { get; }

        protected override INavigatable<CheckNavigatableTypes> CreateViewModel(CheckNavigatableTypes type, AllServices allServices)
        {
            if (type == CheckNavigatableTypes.ShowTransactions)
            {
                return new TransactionsViewModel(() => Navigate(CheckNavigatableTypes.ShowWallets, allServices),
                                                 () => Navigate(CheckNavigatableTypes.ShowCategories, allServices),
                                                 allServices.TransactionService);
            }
            else throw new ArgumentException("Unknown action");
        }

    }
}