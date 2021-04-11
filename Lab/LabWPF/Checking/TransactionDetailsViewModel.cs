using System;
using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Transactions;
using Prism.Mvvm;
using System.IO;
using System.Text.RegularExpressions;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class TransactionDetailsViewModel : BindableBase
    {
        private Transaction _transaction;
        private TransactionsViewModel _tvm;

        public Transaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public decimal Sum
        {
            get { return _transaction.Sum; }
            set 
            {
                _transaction.Sum = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string Currency
        {
            get
            {
                return _transaction.Currency.ToString();
            }
            set 
            {
                _transaction.Currency = (Currencies?)Array.IndexOf(WalletDetailsView.CURRENCIES, value); 
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string Description
        {
            get
            {
                return _transaction.Description;
            }
            set
            {
                _transaction.Description = value;
            }
        }


        public DateTimeOffset? Date
        {
            get { return _transaction.Date; }
            set 
            { 
                _transaction.Date = value;
            }
        }

        public Category Category
        {
            get { return _transaction.Category; }
            set
            {
                if (value != null)
                {
                    _transaction.Category = value;
                }
                else
                {
                    Console.WriteLine("Invalid value of Category!");
                }
            }
        }


        public string DisplayName
        {
            get
            {
                return $"{_transaction.Sum} {_transaction.Currency}";
            }
        }

        public TransactionDetailsViewModel(Transaction transaction, TransactionsViewModel tvm)
        {
            _transaction = transaction;
            _tvm = tvm;
        }

        //private bool IsTransactionEnabled()
        //{
        //    return Regex.IsMatch(Date, @"(\d{4})-(\d{2})-(\d{2})( (\d{2}):(\d{2}):(\d{2}))?$"); ;
        //}

        public void DeleteTransaction()
        {
            _tvm.DeleteTransaction();
        }
    }
}
