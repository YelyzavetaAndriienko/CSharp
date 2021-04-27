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

        public async Task GetWalletsAsync()
        {
            if (!_allServices.CategoryService.CategoriesLoaded)
            {
                await _allServices.CategoryService.GetCategoriesAsync();
                _allServices.CategoryService.CategoriesLoaded = true;
            }
            var dbWallets = await _storage.GetAllAsync(User.Id.ToString("N"));
            foreach (var dbWallet in dbWallets)
            {
                var wallet = new Wallet(User, dbWallet.Guid, dbWallet.Name, dbWallet.InitialBalance, dbWallet.CurrentBalance,
                    dbWallet.MainCurrency, dbWallet.AvailabilityOfCategories);
                for (var i = 0; i < User.CategoriesAmount(); i++)
                {
                    if (dbWallet.AvailabilityOfCategories[i])
                    {
                        wallet.AmountOfAvailableCategories += 1;
                    }
                }
                _wallets.Add(wallet);
                User.MyWallets.Add(wallet);
            }
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
            _allServices.TransactionService.ChangingCurrentWalletNeeded = true;
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