using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LI.CSharp.Lab.Models.Users;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.DataStorage;

namespace LI.CSharp.Lab.Services
{
    public class CategoryService
    {
        private FileDataStorage<DBCategory> _storage = new FileDataStorage<DBCategory>();
        private List<Category> _categories;
        public bool CategoriesLoaded { get; set; }
        public User User { get; }

        public List<Category> Categories
        {
            get
            {
                //_categories.Sort();
                return _categories;
            }
        }

        public async Task GetCategoriesAsync()
        {
            var dbCategories = await _storage.GetAllAsync(User.Id.ToString("N"));
            foreach (var dbCategory in dbCategories)
            {
                var category = new Category(User, dbCategory.Name, dbCategory.Color, dbCategory.Icon, dbCategory.Guid);
                _categories.Add(category);
                User.AddCategory(category);
            }
        }

        public async Task SaveChanges()
        {
            await _storage.DeleteAllFiles(User.Id.ToString("N"));
            foreach (var category in Categories)
            {
                var dbCategory = new DBCategory(category.Name, category.Owner.Id.ToString("N"), category.Description, category.Color, category.Icon, category.Id);
                await _storage.AddOrUpdateAsync(dbCategory);
            }
        }

        public CategoryService(User user)
        {
            User = user;
            _categories = new List<Category>();
            CategoriesLoaded = false;
        }
    }
}