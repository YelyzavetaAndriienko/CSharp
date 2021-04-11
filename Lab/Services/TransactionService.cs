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
        private FileDataStorage<DBTransaction> _storage = new FileDataStorage<DBTransaction>();
        private List<Transaction> _transactions;
        public bool TransactionsLoaded { get; set; }
        // public User User { get; }

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
              //  User.AddTransaction(transaction);
            }

            //return true;
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

        public TransactionService(Wallet wallet)
        {
            Wallet = wallet;
            _transactions = new List<Transaction>();
            TransactionsLoaded = false;
        }
    }
}