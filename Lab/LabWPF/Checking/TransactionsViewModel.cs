﻿using System;
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
            if (!_service.TransactionsLoaded)
            {
                await _service.GetTransactionsAsync();
                _service.TransactionsLoaded = true;
            }
            var ws = new ObservableCollection<TransactionDetailsViewModel>();
            foreach (var transaction in _service.Transactions)
            {
                ws.Add(new TransactionDetailsViewModel(transaction, this));
            }
            Transactions = ws;
        }

        public TransactionsViewModel(Action gotoWallets, Action gotoCategories, TransactionService service)
        {
            _service = service;
            WaitForTransactionsAsync();
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

        public void CreateTransaction()
        {
            transaction = new Transaction(_service.Wallet);
            var goodName = false;
            while (!goodName)
            {
                try
                {
                    transaction.Sum = 0;
                    goodName = true;
                }
                catch (ArgumentException e) { }
            }
            _service.Transactions.Add(transaction);
            _service.Wallet.AddTransaction(transaction, _service.Wallet.Owner.Id);
            TransactionDetailsViewModel wdvm = new TransactionDetailsViewModel(transaction, this);
            Transactions.Add(wdvm);
            CurrentTransaction = wdvm;
        }

        public void DeleteTransaction()
        {
            _service.Transactions.Remove(CurrentTransaction.Transaction);
            _service.Wallet.DeleteTransaction(CurrentTransaction.Transaction.Id, _service.Wallet.Owner.Id);
            Transactions.Remove(CurrentTransaction);
            CurrentTransaction = null;
        }


        //private bool IsTransactionEnabled()
        //{
        //    return !String.IsNullOrWhiteSpace(category.Name) && (category.Name.Length >= 2);
        //}

    }
}