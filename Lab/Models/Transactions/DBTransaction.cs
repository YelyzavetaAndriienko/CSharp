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
        public string Category { get; }

        public DBTransaction(string ownerGuid, decimal sum, Currencies? currency, string description, DateTimeOffset? date, string category, Guid guid)
        {
            OwnerGuid = ownerGuid;
            Sum = sum;
            Currency = currency;
            Description = description;
            Date = date;
            Category = category;
            Guid = guid;
        }
    }
}