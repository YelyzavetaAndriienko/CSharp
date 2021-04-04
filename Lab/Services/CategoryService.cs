using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;

namespace LI.CSharp.Lab.Services
{
    public class CategoryService
    {
        private static User u;
        private static List<Category> c = new List<Category>()
        {
            new Category() {
                Name = "food",
                Description = "new category food",
                Color = "red",
                Icon = new FileInfo("apple")
            },
            new Category() {
                Name = "food",
                Description = "new category food",
                Color = "red",
                Icon = new FileInfo("apple")
            },
            new Category() {
                Name = "food",
                Description = "new category food",
                Color = "red",
                Icon = new FileInfo("apple")
            },
        };

        public List<Category> GetCategories()
        {
            u.Categories = c;
            return c.ToList();
        }
    }
}
