using System;
using System.Collections.Generic;
using System.Linq;
using LI.CSharp.Lab;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Models.Categories;

namespace LI.CSharp.Lab.Models.Users
{
    // Class User keeps name, surname, email address. It can have an unlimited number of wallets and categories.
    public class User : Entity
    {
        private Guid _id;
        private string _name;
        private string _surname;
        private string _email;
        private List<Wallet> _myWallets;
        private List<Wallet> _otherWallets;
        private List<Category> _categories;
        private static int _walletNextNumber = 0;
        private static int _categoryNextNumber = 0;
        public Category DefaultCategory { get; }

        public User()
        {
            _myWallets = new List<Wallet>();
            _otherWallets = new List<Wallet>();
            _categories = new List<Category>();
            DefaultCategory = new Category(this, "DEFAULT", null, null, Guid.Empty);
        }

        public User(Guid id, string name, string surname, string email)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _email = email;
            _myWallets = new List<Wallet>();
            _otherWallets = new List<Wallet>();
            _categories = new List<Category>();
            DefaultCategory = new Category(this, "DEFAULT", null, null, Guid.Empty);
            //_categories.Add(DefaultCategory);
            //AddCategory(DefaultCategory);
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

        public List<Wallet> MyWallets
        {
            get { return _myWallets; }
        }

        public List<Wallet> OtherWallets
        {
            get { return _otherWallets; }
        }

        public void Copy(User other)
        {
            Id = other.Id;
            Name = other.Name;
            Surname = other.Surname;
            Email = other.Email;
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

        public int WalletNextNumber
        {
            get
            {
                var res = _walletNextNumber;
                _walletNextNumber++;
                return res;
            }
        }

        public int CategoryNextNumber
        {
            get
            {
                var res = _categoryNextNumber;
                _categoryNextNumber++;
                return res;
            }
        }

        public Category GetCategory(string name)
        {
            if (name.Equals("DEFAULT"))
                return DefaultCategory;
            return Categories.FirstOrDefault(category => category.Name == name);
        }

        public void AddCategory(Category category)
        {
            Categories.Add(category);
            foreach (var wallet in _myWallets)
            {
                wallet.AvailabilityOfCategories.Add(true);
            }
        }

        public void RemoveCategory(Category category)
        {
            foreach (var wallet in _myWallets)
            {
                wallet.AvailabilityOfCategories.RemoveAt(Categories.IndexOf(category));
                wallet.ChangeCategoryInTransactionsToDefault(category.Name);
            }
            Categories.Remove(category);
        }

        public Wallet GetWallet(Guid walletId)
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

        public Wallet GetOtherWallet(Guid walletId)
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

        public void ShareWallet(User user, Wallet wallet)
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