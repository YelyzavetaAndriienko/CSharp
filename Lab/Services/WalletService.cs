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
        private static User u;
        private static List<Wallet> Users = new List<Wallet>()
        {
            new Wallet(u) {Name = "wal1", CurrentBalance = 57.06m},
            new Wallet(u) {Name = "wal2", CurrentBalance = 157.06m},
            new Wallet(u) {Name = "wal3", CurrentBalance = 257.06m},
            new Wallet(u) {Name = "wal4", CurrentBalance = 357.06m},
            new Wallet(u) {Name = "wal5", CurrentBalance = 457.06m},
        };

        public List<Wallet> GetWallets()
        {
            return Users.ToList();
        }
    }
}
