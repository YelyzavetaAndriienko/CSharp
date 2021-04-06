using System;
using LI.CSharp.Lab.DataStorage;

namespace LI.CSharp.Lab.Models.Wallets
{
    public class DBWallet : IStorable
    {
        public Guid Guid { get; }

        public string OwnerGuid { get; }
        
        public string Name { get; }

        public string Description { get; }

        public decimal InitialBalance { get; }
        public decimal CurrentBalance { get; }
        public Currencies? MainCurrency { get; }

        public DBWallet(string name, string ownerGuid, string description, 
            decimal initialBalance, decimal currentBalance, Currencies? mainCurrency, Guid guid)
        {
            //Guid = Guid.NewGuid();
            Name = name;
            OwnerGuid = ownerGuid;
            Description = description;
            InitialBalance = initialBalance;
            CurrentBalance = currentBalance;
            MainCurrency = mainCurrency;
            Guid = guid;
        }
    }
}