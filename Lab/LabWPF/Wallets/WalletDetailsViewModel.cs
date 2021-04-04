﻿using System;
using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Wallets;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF.Wallets
{
    public class WalletDetailsViewModel : BindableBase
    {
        private Wallet _wallet;
        private WalletsViewModel _wvm;

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
            }
        }

        public decimal CurrentBalance
        {
            get
            {
                return Math.Round(_wallet.CurrentBalance, 2);
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
                //MessageBox.Show(value.ToString());
                RaisePropertyChanged(nameof(DisplayName));
                RaisePropertyChanged(nameof(CurrentBalance));
                RaisePropertyChanged(nameof(InitialBalance));
                //((WalletDetailsView) DataContext)
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
            _wallet = wallet;
            _wvm = wvm;
            //ComboBox0
        }

        public void DeleteWallet()
        {
            _wvm.DeleteWallet();
        }
        
    }
}