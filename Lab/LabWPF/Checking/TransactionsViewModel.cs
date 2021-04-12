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
        private Transaction transaction;
        public Wallet Wallet { get; }
        private Action _gotoCategories;
        private Action _gotoWallets;

        public ObservableCollection<TransactionDetailsViewModel> Transactions
        {
            get
            {
                return _transactions;
            }
            private set
            {
                _transactions = value;
                RaisePropertyChanged();
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
            await _service.GetTransactionsCurrentWalletAsync();
            var ws = new ObservableCollection<TransactionDetailsViewModel>();
            foreach (var transaction in _service.TransactionsCurrentWallet())
            {
                ws.Add(new TransactionDetailsViewModel(transaction, this));
            }
            Transactions = ws;
        }

        public TransactionsViewModel(Action gotoWallets, Action gotoCategories, TransactionService service)
        {
            _service = service;
            Wallet = service.CurrentWallet;
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
            transaction = new Transaction(_service.CurrentWallet);
            transaction.Sum = 0;
            transaction.Currency = _service.CurrentWallet.MainCurrency;
            transaction.Category = _service.CurrentWallet.Owner.Categories.First();
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