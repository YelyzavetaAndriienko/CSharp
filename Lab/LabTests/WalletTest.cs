using System;
using System.Collections.Generic;
using System.IO;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Transactions;
using Xunit;

namespace LI.CSharp.Lab.LabTests
{
    public class WalletTest
    {
        Guid id1 = Guid.NewGuid();
        Guid id2 = Guid.NewGuid();
        Guid id3 = Guid.NewGuid();

        [Fact]
        public void ValidateTest()
        {
            //Arrange
            var owner = new User() { Id = id1, Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };
            var wallet = new Wallet(owner)
            {
                Id = id2,
                Name = "Wallet1",
                InitialBalance = 505.3m,
                Description = "new wallet",
                MainCurrency = Currencies.EUR
            };

            //Act
            var actual = wallet.Validate();

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateNoIdTest()
        {
            //Arrange
            var owner = new User() { Id = id1, Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };
            var wallet = new Wallet(owner)
            {
                Name = "Wallet1",
                InitialBalance = 505.3m,
                Description = "new wallet",
                MainCurrency = Currencies.EUR
            };

            //Act
            var actual = wallet.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoNameTest()
        {
            //Arrange
            var owner = new User() { Id = id1, Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };
            var wallet = new Wallet(owner)
            {
                Id = id2,
                InitialBalance = 505.3m,
                Description = "new wallet",
                MainCurrency = Currencies.EUR
            };

            //Act
            var actual = wallet.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoMainCurrencyTest()
        {
            //Arrange
            var owner = new User() { Id = id1, Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };
            var wallet = new Wallet(owner)
            {
                Id = id2,
                Name = "Wallet1",
                InitialBalance = 505.3m,
                Description = "new wallet"
            };

            //Act
            var actual = wallet.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateEmptyWalletTest()
        {
            //Arrange
            var owner = new User() { Id = id1, Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };
            var wallet = new Wallet(owner);

            //Act
            var actual = wallet.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void AddTransactionTest()
        {
            //Arrange
            var owner = new User()
            {
                Id = id1,
                Name = "Liza",
                Surname = "Andriienko",
                Email = "liza123.sa@gmail.com"
            };
            List<Category> categories = new List<Category>();
            var category = new Category("Food", Colors.Red, "coins.txt");
            categories.Add(category);
            owner.Categories = categories;
            var wallet = new Wallet(owner)
            {
                Id = id2,
                Name = "Wallet1",
                InitialBalance = 505.3m,
                Description = "new wallet"
            };
            var transaction = new Transaction()
            {
                Id = id1,
                Sum = 275.89m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category
            };
            //Act
            wallet.AddTransaction(transaction, owner.Id);
            var actual = (wallet.TransactionsAmount() == 1 &&
                          wallet.CurrentBalance == (wallet.InitialBalance + transaction.Sum));

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void DeleteTransactionTest()
        {
            //Arrange
            var owner = new User()
            {
                Id = id1,
                Name = "Ira",
                Surname = "Matviienko",
                Email = "ira123.sa@gmail.com"
            };
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Color = Colors.Red,
                Icon = new FileInfo("apple")
            };
            List<Category> categories = new List<Category>();
            categories.Add(category);
            owner.Categories = categories;
            Wallet wallet = new Wallet(owner)
            {
                InitialBalance = 505.3m,
                Description = "new wallet",
                MainCurrency = Currencies.EUR
            };
            List<Transaction> transactions = new List<Transaction>();
            var transaction1 = new Transaction()
            {
                Id = id2,
                Sum = 275.89m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category,
                Files = new List<FileInfo>()
            };
            var transaction2 = new Transaction()
            {
                Id = id3,
                Sum = 1.11m,
                Currency = Currencies.UAH,
                Description = "transaction in UAH",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category,
                Files = new List<FileInfo>()
            };
            transactions.Add(transaction1);
            transactions.Add(transaction2);

            //Act
            wallet.Transactions = transactions;
            var actual0 = wallet.DeleteTransaction(id2, id1);
            var actual1 = wallet.TransactionsAmount() == 1;

            //Assert
            Assert.True(actual0);
            Assert.True(actual1);
        }

        [Fact]
        public void EditTransactionTest()
        {
            //Arrange
            var owner = new User()
            {
                Id = id1,
                Name = "Liza",
                Surname = "Andriienko",
                Email = "liza123.sa@gmail.com"
            };
            List<Category> categories = new List<Category>();
            var category0 = new Category("Food", Colors.Red, "coins.txt");
            var category1 = new Category("Restaurants", Colors.Blue, "coins.txt");
            categories.Add(category0);
            categories.Add(category1);
            owner.Categories = categories;
            var wallet = new Wallet(owner)
            {
                Id = id2,
                Name = "Wallet1",
                InitialBalance = 505.3m,
                Description = "new wallet"
            };
            var transaction = new Transaction()
            {
                Id = id3,
                Sum = 275.89m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category0
            };
            //Act
            wallet.AddTransaction(transaction, owner.Id);

            wallet.EditCategoryOfTransaction(id3, category1, id1);
            wallet.EditCurrencyOfTransaction(id3, Currencies.EUR, id1);
            var newDate = new DateTimeOffset(2021, 6, 5, 3, 7, 2, new TimeSpan(2, 0, 0));
            wallet.EditDateOfTransaction(id3, newDate, id1);
            wallet.EditFilesOfTransaction(id3, null, id1);
            wallet.EditSumOfTransaction(id3, 100, id1);

            var copyTr = wallet.GetCopyOfTransaction(id3);
            var actualCategory = copyTr.Category.Equals(category1);
            var actualCurrency = copyTr.Currency == Currencies.EUR;
            var actualDate = copyTr.Date.Equals(newDate);
            var actualSum = copyTr.Sum == 100;

            //Assert
            Assert.True(actualCategory);
            Assert.True(actualCurrency);
            Assert.True(actualDate);
            Assert.True(actualSum);
        }

        [Fact]
        public void CheckSpendingsTest()
        {
            //Arrange
            var owner = new User()
            {
                Id = id1,
                Name = "Liza",
                Surname = "Andriienko",
                Email = "liza123.sa@gmail.com"
            };
            List<Category> categories = new List<Category>();
            var category0 = new Category("Food", Colors.Red, "coins.txt");
            var category1 = new Category("Restaurants", Colors.Blue, "coins.txt");
            categories.Add(category0);
            categories.Add(category1);
            owner.Categories = categories;
            var wallet = new Wallet(owner)
            {
                Id = id2,
                Name = "Wallet1",
                InitialBalance = 0,
                Description = "new wallet",
                MainCurrency = Currencies.USD
            };
            var transaction1 = new Transaction()
            {
                Id = id1,
                Sum = 275.5m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 3, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category0
            };
            var transaction2 = new Transaction()
            {
                Id = id2,
                Sum = 83.1m,
                Currency = Currencies.UAH,
                Description = "transaction in UAH",
                Date = new DateTimeOffset(2021, 3, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category1,
                Files = new List<FileInfo>()
            };
            var transaction3 = new Transaction()
            {
                Id = id3,
                Sum = -95.2m,
                Currency = Currencies.EUR,
                Description = "transaction in EUR",
                Date = new DateTimeOffset(2021, 3, 22, 18, 20, 5, new TimeSpan(2, 0, 0)),
                Category = category0
            };

            //Act
            wallet.AddTransaction(transaction1, owner.Id);
            wallet.AddTransaction(transaction2, owner.Id);
            wallet.AddTransaction(transaction3, owner.Id);

            var actualIncome = wallet.GeneralSumOfIncomeForMonth() == (decimal)278.5;
            var actualSpendings = wallet.GeneralSumOfSpendingsForMonth() == (decimal)80;

            //Assert
            Assert.True(actualIncome);
            Assert.True(actualSpendings);
        }
    }
}