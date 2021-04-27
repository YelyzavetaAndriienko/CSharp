using System;
using System.IO;
using Xunit;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Models.Users;

namespace LI.CSharp.Lab.LabTests
{
    public class CategoryTest
    {
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

            //Act
            var actual = category.Validate();

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateNoNameTest()
        {
            //Arrange
            var user = new User() { Id = Guid.NewGuid(), Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category(user)
            {
                Description = "new category food",
                Color = Colors.Red,
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
            var user = new User() { Id = Guid.NewGuid(), Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category(user)
            {
                Name = "food",
                Description = "new category food",
                Icon = new FileInfo("apple")
            };

            //Act
            var actual = category.Validate();

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateNoIconTest()
        {
            //Arrange
            var user = new User() { Id = Guid.NewGuid(), Name = "Ira", Surname = "Matviienko", Email = "ira123.sa@gmail.com" };
            var category = new Category(user)
            {
                Name = "food",
                Description = "new category food",
                Color = Colors.Red,
            };

            //Act
            var actual = category.Validate();

            //Assert
            Assert.True(actual);
        }
    }
}
