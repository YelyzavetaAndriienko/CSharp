using System;
using System.IO;
using Xunit;

namespace LI.CSharp.Lab.LabTests
{
    public class CategoryTest
    {
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

            //Act
            var actual = category.Validate();

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateNoNameTest()
        {
            //Arrange
            var category = new Category()
            {
                Description = "new category food",
                Color = "red",
                Icon = new FileInfo("apple")
            };

            //Act
            var actual = category.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoColorTest()
        {
            //Arrange
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Icon = new FileInfo("apple")
            };

            //Act
            var actual = category.Validate();

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateNoIconTest()
        {
            //Arrange
            var category = new Category()
            {
                Name = "food",
                Description = "new category food",
                Color = "red",
            };

            //Act
            var actual = category.Validate();

            //Assert
            Assert.False(actual);
        }
    }
}
