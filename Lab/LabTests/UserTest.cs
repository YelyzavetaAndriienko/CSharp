using System;
using System.Collections.Generic;
using System.IO;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab;
using LI.CSharp.Lab.Models.Wallets;
using Xunit;

namespace LI.CSharp.Lab.LabTests
{
    public class UserTest
    {
        Guid id = Guid.NewGuid();
        Guid id1 = Guid.NewGuid();
        Guid id2 = Guid.NewGuid();
        Guid id3 = Guid.NewGuid();
        Guid id4 = Guid.NewGuid();

        [Fact]
        public void ShareWalletTest()
        {
            //Arrange
            var user1 = new User() { Id = id1, Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };
            var user2 = new User() { Id = id2, Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Color = "red",
                Icon = new FileInfo("apple")
            };
            Wallet wallet = new Wallet(user1)
            {
                Id = id,
                InitialBalance = 505.3m,
                Description = "new wallet",
                MainCurrency = Currencies.EUR
            };
            List<Transaction> transactions = new List<Transaction>();
            var transaction1 = new Transaction()
            {
                Id = id3,
                Sum = 275.89m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category,
                Files = new List<FileInfo>()
            };
            var transaction2 = new Transaction()
            {
                Id = id4,
                Sum = 1.11m,
                Currency = Currencies.UAH,
                Description = "transaction in UAH",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category,
                Files = new List<FileInfo>()
            };
            transactions.Add(transaction1);
            transactions.Add(transaction2);
            wallet.Transactions = transactions;

            user1.ShareWallet(user2, wallet);

            //Act
            var actual1 = user2.OtherWallets.Contains(wallet);
            var actual2 = wallet.UsersId.Contains(user2.Id);

            //Assert
            Assert.True(actual1);
            Assert.True(actual2);
        }

        [Fact]
        public void FullNameTest()
        {
            //Arrange
            var user = new User { Name = "Liza", Surname = "Andriienko" };
            var expected = "Andriienko Liza";

            //Act
            var actual = user.FullName;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FullNameNoNameTest()
        {
            //Arrange
            var user = new User { Name = "Liza" };
            var expected = "Liza";

            //Act
            var actual = user.FullName;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FullNameNoSurnameTest()
        {
            //Arrange
            var user = new User { Surname = "Andriienko" };
            var expected = "Andriienko";

            //Act
            var actual = user.FullName;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateTest()
        {
            //Arrange
            var user = new User() { Id = id1, Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };

            //Act
            var actual = user.Validate();

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateNoIdTest()
        {
            //Arrange
            var user = new User() { Name = "Liza", Surname = "Andriienko", Email = "liza123.sa@gmail.com" };

            //Act
            var actual = user.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoNameTest()
        {
            //Arrange
            var user = new User() { Id = id1, Email = "liza123.sa@gmail.com" };

            //Act
            var actual = user.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoEmailTest()
        {
            //Arrange
            var user = new User() { Id = id1, Name = "Liza", Surname = "Andriienko" };

            //Act
            var actual = user.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateEmptyUserTest()
        {
            //Arrange
            var user = new User();

            //Act
            var actual = user.Validate();

            //Assert
            Assert.False(actual);
        }
    }
}
