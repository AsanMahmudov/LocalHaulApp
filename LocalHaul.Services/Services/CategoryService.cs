using Data.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    internal class CategoryService : ICategoryService
    {
        public Task<Guid> CreateCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllCategoriesForAdminAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RestoreCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(Guid id, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
