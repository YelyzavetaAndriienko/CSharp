using System;
using System.IO;
using Xunit;

namespace LI.CSharp.Lab.LabTests
{
    public class TransactionTest
    {
        Guid id1 = Guid.NewGuid();
        [Fact]
        public void ValidateTest()
        {
            //Arrange
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Color = "red",
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
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Color = "red",
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
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Color = "red",
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
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Color = "red",
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
            Assert.False(actual);
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
