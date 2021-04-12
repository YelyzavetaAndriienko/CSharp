using System;
using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Transactions;
using Prism.Mvvm;
using System.IO;
using System.Text.RegularExpressions;
using LI.CSharp.Lab.Models.Wallets;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public class TransactionDetailsViewModel : BindableBase
    {
        private Transaction _transaction;
        public TransactionsViewModel Tvm;

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

        public string Category
        {
            get { return _transaction.Category.Name; }
            set
            {
                _transaction.Category = Tvm.Wallet.Owner.GetCategory(value);
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
            Tvm = tvm;
        }

        //private bool IsTransactionEnabled()
        //{
        //    return Regex.IsMatch(Date, @"(\d{4})-(\d{2})-(\d{2})( (\d{2}):(\d{2}):(\d{2}))?$"); ;
        //}

        public void DeleteTransaction()
        {
            Tvm.DeleteTransaction();
        }
    }
}
