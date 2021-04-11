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
        private TransactionsViewModel _wvm;

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

        public Currencies? Currency
        {
            get { return _transaction.Currency; }
            set 
            { 
                _transaction.Currency = value; 
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
                RaisePropertyChanged(nameof(DisplayName));
            }
        }


        public DateTimeOffset? Date
        {
            get { return _transaction.Date; }
            set 
            { 
                _transaction.Date = value; 
                RaisePropertyChanged(nameof(DisplayName));
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
                    RaisePropertyChanged(nameof(DisplayName));
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

        public TransactionDetailsViewModel(Transaction transaction, TransactionsViewModel wvm = null)
        {
            _transaction = transaction;
            _wvm = wvm;
        }

        //private bool IsTransactionEnabled()
        //{
        //    return Regex.IsMatch(Date, @"(\d{4})-(\d{2})-(\d{2})( (\d{2}):(\d{2}):(\d{2}))?$"); ;
        //}

        public void DeleteTransaction()
        {
            _wvm.DeleteTransaction();
        }
    }
}
