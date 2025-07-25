using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// Defines the contract for category-related business operations.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all active (not soft-deleted) categories.
        /// Global query filter will ensure IsDeleted = false is applied.
        /// </summary>
        /// <returns>A collection of Category entities.</returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        /// <summary>
        /// Retrieves a specific active category by its unique identifier.
        /// Global query filter will ensure IsDeleted = false is applied.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>The Category entity if found, otherwise null.</returns>
        Task<Category> GetCategoryByIdAsync(Guid id);

        // Admin-specific methods
        /// <summary>
        /// Retrieves all categories, including soft-deleted ones (ignores global query filter).
        /// For administrative purposes.
        /// </summary>
        /// <returns>A collection of all Category entities.</returns>
        Task<IEnumerable<Category>> GetAllCategoriesForAdminAsync();

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryName">The name of the new category.</param>
        /// <returns>The Id of the newly created category.</returns>
        Task<Guid> CreateCategoryAsync(string categoryName);

        /// <summary>
        /// Updates an existing category's name.
        /// </summary>
        /// <param name="id">The unique identifier of the category to update.</param>
        /// <param name="newName">The new name for the category.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateCategoryAsync(Guid id, string newName);

        /// <summary>
        /// Soft-deletes a category by setting its IsDeleted flag to true.
        /// </summary>
        /// <param name="id">The unique identifier of the category to soft-delete.</param>
        Task SoftDeleteCategoryAsync(Guid id);

        /// <summary>
        /// Restores a soft-deleted category by setting its IsDeleted flag to false.
        /// </summary>
        /// <param name="id">The unique identifier of the category to restore.</param>
        Task RestoreCategoryAsync(Guid id);
    }
}
