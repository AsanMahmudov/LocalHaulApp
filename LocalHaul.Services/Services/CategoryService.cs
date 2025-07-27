using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LocalHaulDbContext _dbContext;
        public CategoryService(LocalHaulDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateCategoryAsync(string categoryName)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(), // Generate a unique ID for the new category
                Name = categoryName,
                IsDeleted = false // New categories are not soft-deleted by default
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category.Id; // Return the ID of the newly created category
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesForAdminAsync()
        {
            return await _dbContext.Categories
                                   .IgnoreQueryFilters() // Bypass the IsDeleted filter for admin view
                                   .ToListAsync();
        }   
        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }


        public async Task RestoreCategoryAsync(Guid id)
        {
            // Retrieve category, ignoring filters, as it must be found regardless of its IsDeleted state
            var category = await _dbContext.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.IsDeleted = false; // Restore
                _dbContext.Categories.Update(category); // Mark entity as modified
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteCategoryAsync(Guid id)
        {
            // Retrieve category, ignoring filters, as it might already be soft-deleted or need soft-deleting
            var category = await _dbContext.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.IsDeleted = true; // Mark as soft-deleted
                _dbContext.Categories.Update(category); // Mark entity as modified
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateCategoryAsync(Guid id, string newName)
        {
            // Retrieve category, ignoring filters in case it was soft-deleted but needs renaming by admin
            var category = await _dbContext.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return false; // Category not found
            }

            category.Name = newName;
            _dbContext.Categories.Update(category); // Mark entity as modified
            await _dbContext.SaveChangesAsync();
            return true; // Update successful
        }
    }
}
