using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Transactions;
using LI.CSharp.Lab.DataStorage;

namespace LI.CSharp.Lab.Services
{
    public class TransactionService
    {
        private List<TrService> _trServices;
        public Wallet CurrentWallet { get; set; }

        public List<Transaction> TransactionsCurrentWallet()
        {
            foreach (var trService in _trServices.Where(trService => trService.Wallet.Id == CurrentWallet.Id))
            {
                return trService.Transactions;
            }
            var trSer = new TrService(CurrentWallet);
            _trServices.Add(trSer);
            return trSer.Transactions;
            //throw new ArgumentException("Current wallet is null!");
        }
        
        public async Task GetTransactionsCurrentWalletAsync()
        {
            foreach (var trService in _trServices.Where(trService => trService.Wallet.Id == CurrentWallet.Id))
            {
                if (!trService.TransactionsLoaded)
                {
                    await trService.GetTransactionsAsync();
                    trService.TransactionsLoaded = true;
                }
                return;
            }
            var trSer = new TrService(CurrentWallet);
            //Category category = new Category()
            //trSer.Transactions.Add(new Transaction(CurrentWallet, Guid.NewGuid(), 125, Currencies.EUR, DateTimeOffset.Now, null));
            _trServices.Add(trSer);
            await trSer.GetTransactionsAsync();
            trSer.TransactionsLoaded = true;
        }
        
        public async Task SaveChanges()
        {
            foreach (var trService in _trServices)
            {
                await trService.SaveChanges();
            }
        }

        public TransactionService()
        {
            CurrentWallet = null;
            _trServices = new List<TrService>();
        }
    }
}