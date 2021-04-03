using System;
using System.Collections.Generic;
using LI.CSharp.Lab;

namespace LI.CSharp.Lab.Models.Users
{
    // Class User keeps name, surname, email address. It can have an unlimited number of wallets and categories.
    public class User : Entity
    {
        private Guid _id;
        private string _name;
        private string _surname;
        private string _email;
        private List<WalletOld> _myWallets;
        private List<WalletOld> _otherWallets;
        private List<Category> _categories;

        public User()
        {
            _myWallets = new List<WalletOld>();
            _otherWallets = new List<WalletOld>();
            _categories = new List<Category>();
        }

        public User(Guid id, string name, string surname, string email)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _email = email;
            _myWallets = new List<WalletOld>();
            _otherWallets = new List<WalletOld>();
            _categories = new List<Category>();
            if (!Validate())
            {
                throw new ArgumentException("Invalid argument in constructor of User!");
            }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
                else
                {
                    Console.WriteLine("Invalid value of Name!");
                }
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _surname = value;
                }
                else
                {
                    Console.WriteLine("Invalid value of Surname!");
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _email = value;
                }
                else
                {
                    Console.WriteLine("Invalid value of Email!");
                }
            }
        }

        public List<WalletOld> MyWallets
        {
            get { return _myWallets; }
        }

        public List<WalletOld> OtherWallets
        {
            get { return _otherWallets; }
        }

        public List<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        public int CategoriesAmount()
        {
            return _categories.Count;
        }

        public int MyWalletsAmount()
        {
            return _myWallets.Count;
        }

        public WalletOld GetWallet(Guid walletId)
        {
            foreach (var wallet in MyWallets)
            {
                if (wallet.Id == walletId)
                {
                    return wallet;
                }
            }
            Console.WriteLine("The wallet is not found");
            return null;
        }

        public WalletOld GetOtherWallet(Guid walletId)
        {
            foreach (var wallet in OtherWallets)
            {
                if (wallet.Id == walletId)
                {
                    return wallet;
                }
            }
            Console.WriteLine("The wallet is not found");
            return null;
        }

        public void ShareWallet(User user, WalletOld wallet)
        {
            user.OtherWallets.Add(wallet);
            wallet.UsersId.Add(user.Id);
        }

        public string FullName
        {
            get
            {
                var result = Surname;
                if (!String.IsNullOrWhiteSpace(Name))
                {
                    if (!String.IsNullOrWhiteSpace(Surname))
                    {
                        result += ' ';
                    }
                    result += Name;
                }
                return result;
            }
        }

        public override bool Validate()
        {
            var result = true;

            if (Id == Guid.Empty)
                result = false;
            if (String.IsNullOrWhiteSpace(Name))
                result = false;
            if (String.IsNullOrWhiteSpace(Surname))
                result = false;
            if (String.IsNullOrWhiteSpace(Email))
                result = false;

            return result;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}