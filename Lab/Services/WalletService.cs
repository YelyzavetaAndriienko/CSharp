using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LI.CSharp.Lab.DataStorage;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;

namespace LI.CSharp.Lab.Services
{
    public class WalletService
    {
        private FileDataStorage<DBWallet> _storage = new FileDataStorage<DBWallet>();
        private List<Wallet> _wallets;
        public bool WalletsLoaded { get; set; }
        public User User { get; }

        private AllServices _allServices;

        public List<Wallet> Wallets
        {
            get
            {
                _wallets.Sort();
                return _wallets;
            }
        }

        public AllServices AllServices
        {
            get
            {
                return _allServices;
            }
        }

        /*public static User u = new User();
        private static List<Wallet> Wallets = new List<Wallet>()
        {
            new Wallet(u) {Name = "wal1", InitialBalance = 57.06m, MainCurrency = Currencies.UAH},
            new Wallet(u) {Name = "wal2", InitialBalance = 157.06m, MainCurrency = Currencies.EUR},
            new Wallet(u) {Name = "wal3", InitialBalance = 257.06m, MainCurrency = Currencies.UAH},
            new Wallet(u) {Name = "wal4", InitialBalance = 357.06m, MainCurrency = Currencies.UAH},
            new Wallet(u) {Name = "wal5", InitialBalance = 457.06m, MainCurrency = Currencies.USD},
        };*/

        public async Task GetWalletsAsync()
        {
            var dbWallets = await _storage.GetAllAsync(User.Id.ToString("N"));
            foreach (var dbWallet in dbWallets)
            {
                var wallet = new Wallet(User, dbWallet.Guid, dbWallet.Name, dbWallet.InitialBalance,
                    dbWallet.MainCurrency, dbWallet.AvailabilityOfCategories);
                _wallets.Add(wallet);
                User.MyWallets.Add(wallet);
            }

            //return true;
        }

        public async Task SaveChanges()
        {
            await _storage.DeleteAllFiles(User.Id.ToString("N"));
            foreach (var wallet in Wallets)
            {
                var dbWallet = new DBWallet(wallet.Name, wallet.Owner.Id.ToString("N"),
                    wallet.Description, wallet.InitialBalance,
                    wallet.CurrentBalance, wallet.MainCurrency,
                    wallet.Id, wallet.AvailabilityOfCategories);
                await _storage.AddOrUpdateAsync(dbWallet);
            }
        }

        public void SetCurrentWalletInTransactionService(Wallet wallet)
        {
            _allServices.TransactionService.CurrentWallet = wallet;
        }

        public WalletService(User user, AllServices allServices)
        {
            User = user;
            _allServices = allServices;
            _wallets = new List<Wallet>();
            WalletsLoaded = false;
        }
    }
}