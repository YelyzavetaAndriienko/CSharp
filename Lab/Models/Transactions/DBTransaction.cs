using System;
using System.IO;
using LI.CSharp.Lab.DataStorage;
using LI.CSharp.Lab.Models.Categories;

namespace LI.CSharp.Lab.Models.Transactions
{
    public class DBTransaction : IStorable
    {
        public Guid Guid { get; }
        public string OwnerGuid { get; }
        public decimal Sum { get; }
        public Currencies? Currency { get; }
        public string Description { get; }
        public DateTimeOffset? Date { get; }
        public Category Category { get; }

        public DBTransaction(string walletGuid, decimal sum, Currencies? currency, string description, DateTimeOffset? date, Category category, Guid guid)
        {
            Guid = guid;
            Description = description;
            Sum = sum;
            Currency = currency;
            Date = date;
            Category = category;
            OwnerGuid = walletGuid;
        }
    }
}