using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;

namespace LI.CSharp.Lab.Services
{
    public class WalletService
    {
        private static User u = new User();
        private static List<Wallet> Users = new List<Wallet>()
        {
            new Wallet(u) {Name = "wal1", InitialBalance = 57.06m, MainCurrency = Currencies.UAH},
            new Wallet(u) {Name = "wal2", InitialBalance = 157.06m, MainCurrency = Currencies.EUR},
            new Wallet(u) {Name = "wal3", InitialBalance = 257.06m, MainCurrency = Currencies.UAH},
            new Wallet(u) {Name = "wal4", InitialBalance = 357.06m, MainCurrency = Currencies.UAH},
            new Wallet(u) {Name = "wal5", InitialBalance = 457.06m, MainCurrency = Currencies.USD},
        };

        public List<Wallet> GetWallets()
        {
            return Users.ToList();
        }
    }
}