using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Transactions;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class TransactionsViewModel : BindableBase, INavigatable<CheckNavigatableTypes>
    {
        private TransactionService _service { get; }
        private TransactionDetailsViewModel _currentTransaction;
        public ObservableCollection<TransactionDetailsViewModel> _transactions;
        //private Transaction transaction;
        public Wallet _wallet;
        private Action _gotoCategories;
        private Action _gotoWallets;
        private bool _showedFirstly;

        public ObservableCollection<TransactionDetailsViewModel> Transactions
        {
            get
            {
                if (_service.ChangingCurrentWalletNeeded)
                {
                    _service.ChangingCurrentWalletNeeded = false;
                    if (!_showedFirstly)
                    {
                        CurrentTransaction = null;
                        RaisePropertyChanged(nameof(CurrentTransaction));
                        _wallet = _service.CurrentWallet;
                        WaitForTransactionsAsync();
                    }
                    else _showedFirstly = false;
                }
                return _transactions;
            }
            private set
            {
                _transactions = value;
                RaisePropertyChanged();
            }
        }

        public Wallet Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public TransactionDetailsViewModel CurrentTransaction
        {
            get
            {
                return _currentTransaction;
            }
            set
            {
                _currentTransaction = value;
                RaisePropertyChanged();
                //OnPropertyChanged();
            }
        }

        private async void WaitForTransactionsAsync()
        {
            var ws = new ObservableCollection<TransactionDetailsViewModel>();
            await _service.GetTransactionsCurrentWalletAsync();
            foreach (var tr in _service.TransactionsCurrentWallet())
            {
                ws.Add(new TransactionDetailsViewModel(tr, this));
            }
            _transactions = ws;
            RaisePropertyChanged(nameof(Transactions));
        }

        public TransactionsViewModel(Action gotoWallets, Action gotoCategories, TransactionService service)
        {
            _service = service;
            _wallet = service.CurrentWallet;
            _showedFirstly = true;
            WaitForTransactionsAsync();
            _gotoWallets = gotoWallets;
            WalletsCommand = new DelegateCommand(_gotoWallets);
            _gotoCategories = gotoCategories;
            CategoriesCommand = new DelegateCommand(_gotoCategories);
        }

        public CheckNavigatableTypes Type
        {
            get
            {
                return CheckNavigatableTypes.ShowTransactions;
            }
        }

        public void ClearSensitiveData()
        {

        }
        
        public DelegateCommand WalletsCommand { get; }
        public DelegateCommand CategoriesCommand { get; }

        public void CreateTransaction()
        {
            Transaction transaction = new Transaction(_service.CurrentWallet);
            transaction.Sum = 0;
            transaction.Currency = _service.CurrentWallet.MainCurrency;
            transaction.Category = _service.CurrentWallet.AmountOfAvailableCategories == 0 ? 
                _service.CurrentWallet.Owner.DefaultCategory : 
                _service.CurrentWallet.GetFirstAvailableCategory();
            _service.TransactionsCurrentWallet().Add(transaction);
            _service.CurrentWallet.AddTransaction(transaction, _service.CurrentWallet.Owner.Id);
            TransactionDetailsViewModel tdvm = new TransactionDetailsViewModel(transaction, this);
            Transactions.Add(tdvm);
            CurrentTransaction = tdvm;
        }

        public void DeleteTransaction()
        {
            _service.TransactionsCurrentWallet().Remove(CurrentTransaction.Transaction);
            _service.CurrentWallet.DeleteTransaction(CurrentTransaction.Transaction.Id, _service.CurrentWallet.Owner.Id);
            Transactions.Remove(CurrentTransaction);
            CurrentTransaction = null;
        }


        //private bool IsTransactionEnabled()
        //{
        //    return !String.IsNullOrWhiteSpace(category.Name) && (category.Name.Length >= 2);
        //}

    }
}