using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LI.CSharp.Lab.DataStorage;
using LI.CSharp.Lab.Models.Transactions;
using LI.CSharp.Lab.Models.Wallets;

namespace LI.CSharp.Lab.Services
{
    public class TrService
    {
        private FileDataStorage<DBTransaction> _storage = new FileDataStorage<DBTransaction>();
        private List<Transaction> _transactions;
        public bool TransactionsLoaded { get; set; }

        public Wallet Wallet { get; }

        public List<Transaction> Transactions
        {
            get
            {
                _transactions.Sort();
                return _transactions;
            }
        }

        public async Task GetTransactionsAsync()
        {
            var dbTransactions = await _storage.GetAllAsync(Wallet.Id.ToString("N"));
            foreach (var dbTransaction in dbTransactions)
            {
                var transaction = new Transaction(Wallet, dbTransaction.Guid, dbTransaction.Sum, dbTransaction.Currency, dbTransaction.Date, dbTransaction.Category);
                _transactions.Add(transaction);
            }
        }

        public async Task SaveChanges()
        {
            await _storage.DeleteAllFiles(Wallet.Id.ToString("N"));
            foreach (var transaction in _transactions)
            {
                var dbTransaction = new DBTransaction(transaction.Wallet.Id.ToString("N"), transaction.Sum, transaction.Currency, transaction.Description, transaction.Date, transaction.Category, transaction.Id);
                await _storage.AddOrUpdateAsync(dbTransaction);
            }
        }

        public TrService(Wallet wallet)
        {
            Wallet = wallet;
            _transactions = new List<Transaction>();
            TransactionsLoaded = false;
        }
    }
}