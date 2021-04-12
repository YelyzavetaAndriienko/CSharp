using System;
using System.Collections.Generic;
using System.IO;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;

namespace LI.CSharp.Lab.Models.Transactions
{
    // Class Transaction keeps an amount, currency, category, description, date.
    //You can also attach files (images or text) to the transaction.
    public class Transaction : Entity, IComparable<Transaction>
    {
        private Wallet _wallet;
        private Guid _id;
        private decimal _sum;
        private Currencies? _currency;
        private string _description;
        private DateTimeOffset? _date;
        private Category _category;
        private List<FileInfo> _files;

        public Transaction()
        {
            _files = new List<FileInfo>();
        }

        public Transaction(Wallet wallet, Guid id, decimal sum, Currencies? currency, DateTimeOffset? date, Category category)
        {
            _wallet = wallet;
            _id = id;
            _sum = sum;
            _currency = currency;
            _date = date;
            _category = category;
            _files = new List<FileInfo>();
            //if (!Validate())
            //{
            //    throw new ArgumentException("Invalid argument in constructor of Transaction!");
           // }
        }

        public Transaction(Wallet wallet)
        {
            _id = Guid.NewGuid();
            _wallet = wallet;
        }

        public Wallet Wallet
        {
            get { return _wallet; }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public decimal Sum
        {
            get { return _sum; }
            set { _sum = value; }
        }

        public Currencies? Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTimeOffset? Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Category Category
        {
            get { return _category; }
            set
            {
                if (value != null)
                {
                    _category = value;
                }
                else
                {
                    Console.WriteLine("Invalid value of Category!");
                }
            }
        }

        public List<FileInfo> Files
        {
            get { return _files; }
            set { _files = value; }
        }

        public void AddFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            _files.Add(fileInfo);
        }

        public override bool Validate()
        {
            var result = true;

            if (Id == Guid.Empty)
                result = false;
            if (Date == null)
                result = false;
            if (Currency == null)
                result = false;
            if (Category == null)
                result = false;

            return result;
        }

        public int CompareTo(Transaction? other)
        {
            if (Date == null)
                return 1;
            if (other == null || other.Date == null) 
                return -1;
            return DateTimeOffset.Compare((DateTimeOffset) Date, (DateTimeOffset)other.Date);
        }

        public override string ToString()
        {
            if (Description != null)
            {
                return $"{Sum}, {Currency}, {Description}, {Date}, Category ({Category})";
            }
            else
            {
                return $"{Sum}, {Currency}, {Date}, Category ({Category})";
            }
        }
    }
}