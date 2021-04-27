using System;
using System.IO;
using Xunit;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Transactions;
using LI.CSharp.Lab.Models.Users;

namespace LI.CSharp.Lab.LabTests
{
    public class TransactionTest
    {
        Guid id1 = Guid.NewGuid();
        [Fact]
        public void ValidateTest()
        {
            //Arrange
            var user = new User() { Id = Guid.NewGuid(), Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category(user)
            {
                Name = "food",
                Description = "new category food",
                Color = Colors.Red,
                Icon = new FileInfo("apple")
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
            var actual = transaction.Validate();

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateNoIdTest()
        {
            //Arrange
            var user = new User() { Id = Guid.NewGuid(), Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category(user)
            {
                Name = "food",
                Description = "new category food",
                Color = Colors.Red,
                Icon = new FileInfo("apple")
            };
            var transaction = new Transaction()
            {
                Sum = 275.89m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category
            };

            //Act
            var actual = transaction.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoCurrencyTest()
        {
            //Arrange
            var user = new User() { Id = Guid.NewGuid(), Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category(user)
            {
                Name = "food",
                Description = "new category food",
                Color = Colors.Red,
                Icon = new FileInfo("apple")
            };
            var transaction = new Transaction()
            {
                Id = id1,
                Sum = 275.89m,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0)),
                Category = category
            };

            //Act
            var actual = transaction.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoDateTest()
        {
            //Arrange
            var user = new User() { Id = Guid.NewGuid(), Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category(user)
            {
                Name = "food",
                Description = "new category food",
                Color = Colors.Red,
                Icon = new FileInfo("apple")
            };
            var transaction = new Transaction()
            {
                Id = id1,
                Sum = 275.89m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Category = category
            };

            //Act
            var actual = transaction.Validate();

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateNoCategoryTest()
        {
            //Arrange
            var transaction = new Transaction()
            {
                Id = id1,
                Sum = 275.89m,
                Currency = Currencies.USD,
                Description = "new transaction",
                Date = new DateTimeOffset(2021, 7, 20, 14, 10, 5, new TimeSpan(2, 0, 0))
            };

            //Act
            var actual = transaction.Validate();

            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void ValidateEmptyUserTest()
        {
            //Arrange
            var transaction = new Transaction();

            //Act
            var actual = transaction.Validate();

            //Assert
            Assert.False(actual);
        }
    }
}
