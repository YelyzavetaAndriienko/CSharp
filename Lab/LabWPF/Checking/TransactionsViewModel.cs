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
using System.Text.RegularExpressions;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class TransactionsViewModel : BindableBase, INavigatable<CheckNavigatableTypes>
    {
        private TransactionService _service { get; }
        private TransactionDetailsViewModel _currentTransaction;
        private ObservableCollection<TransactionDetailsViewModel> _transactions;
        //private Transaction transaction;
        private Wallet _wallet;
        private Action _gotoCategories;
        private Action _gotoWallets;
        private bool _showedFirstly;
        private int _firstTransactionNumber = 2;
        private int _lastTransactionNumber = 3;
      //  private ObservableCollection<TransactionDetailsViewModel> ws;

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
                        // WaitForTransactionsAsync();
                        ShowTransactions();
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

        public int FirstTransactionNumber
        {
            get
            {
                return _firstTransactionNumber;
            }
            set
            {
                _firstTransactionNumber = value;
                RaisePropertyChanged();
            }
        }

        public int LastTransactionNumber
        {
            get
            {
                return _lastTransactionNumber;
            }
            set
            {
                _lastTransactionNumber = value;
                RaisePropertyChanged();
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
           // WaitForTransactionsAsync();
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
        
        public async void ShowTransactions()
        {
            await _service.GetTransactionsCurrentWalletAsync();
            var ws = new ObservableCollection<TransactionDetailsViewModel>();
            if (IsNumbersEnabled())
            {
                if ((LastTransactionNumber - FirstTransactionNumber) <= 10 && (LastTransactionNumber - FirstTransactionNumber) >= 0)
                {
                    int i = 1;
                    foreach (var transaction in _service.TransactionsCurrentWallet())
                    {
                        if (i <= LastTransactionNumber)
                        {
                            ws.Add(new TransactionDetailsViewModel(transaction, this));
                            i++;
                        }
                    }
                }
                else
                {
                    int i = 1;
                    foreach (var transaction in _service.TransactionsCurrentWallet())
                    {
                        if (i <= 10)
                        {
                            ws.Add(new TransactionDetailsViewModel(transaction, this));
                            i++;
                        }
                    }
                }

                var wsNew = new ObservableCollection<TransactionDetailsViewModel>();
                int first = FirstTransactionNumber - 1;
                int last = LastTransactionNumber - 1;
                if (last >= ws.Count())
                {
                    last = ws.Count() - 1;
                }
                for (int n = first; n <= last; n++)
                {
                    wsNew.Add(ws.ElementAt(n));
                }

                Transactions = wsNew;
                RaisePropertyChanged(nameof(Transactions));
            }
            else
            {
                MessageBox.Show("Please enter correct numbers");
            }
        }


        private bool IsNumbersEnabled()
        {
            return Regex.IsMatch(FirstTransactionNumber.ToString(), @"\d+") 
                && Regex.IsMatch(LastTransactionNumber.ToString(), @"\d+")
                && (FirstTransactionNumber > 0)
                && (FirstTransactionNumber <= LastTransactionNumber)
                && ((LastTransactionNumber - FirstTransactionNumber) <= 10);
        }


    }
}