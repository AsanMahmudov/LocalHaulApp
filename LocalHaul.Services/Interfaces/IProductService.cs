using Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all active (not soft-deleted) product listings.
        /// Global query filter will ensure IsDeleted = false is applied.
        /// </summary>
        /// <returns>A collection of Product entities.</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a specific active product listing by its unique identifier.
        /// Global query filter will ensure IsDeleted = false is applied.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The Product entity if found, otherwise null.</returns>
        Task<Product> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Creates a new product listing, including saving an associated image.
        /// </summary>
        /// <param name="product">The Product entity to create.</param>
        /// <param name="imageFile">The image file uploaded by the user (IFormFile).</param>
        Task CreateProductAsync(Product product, IFormFile imageFile);

        /// <summary>
        /// Updates an existing product listing, with an option to update its image.
        /// </summary>
        /// <param name="product">The Product entity with updated data.</param>
        /// <param name="imageFile">An optional new image file. If null, existing images are retained.</param>
        Task UpdateProductAsync(Product product, IFormFile? imageFile);

        /// <summary>
        /// Soft-deletes a product listing by setting its IsDeleted flag to true.
        /// </summary>
        /// <param name="id">The unique identifier of the product to soft-delete.</param>
        Task SoftDeleteProductAsync(Guid id);

        /// <summary>
        /// Searches for products by a search term and/or filters by category.
        /// Respects soft-delete status via global query filter.
        /// </summary>
        /// <param name="searchTerm">Optional: text to search in title/description.</param>
        /// <param name="categoryId">Optional: category ID to filter by.</param>
        /// <returns>A collection of filtered and/or searched Product entities.</returns>
        Task<IEnumerable<Product>> SearchAndFilterProductsAsync(string? searchTerm, Guid? categoryId);

        // Admin-specific methods (would typically be in IAdminProductService if you extend admin features significantly)
        /// <summary>
        /// Retrieves all product listings, including soft-deleted ones (ignores global query filter).
        /// For administrative purposes.
        /// </summary>
        /// <returns>A collection of all Product entities.</returns>
        Task<IEnumerable<Product>> GetAllProductsForAdminNoQueryFilterAsync();

        /// <summary>
        /// Restores a soft-deleted product by setting its IsDeleted flag to false.
        /// For administrative purposes.
        /// </summary>
        /// <param name="id">The unique identifier of the product to restore.</param>
        Task RestoreProductAsync(Guid id);

        /// <summary>
        /// Permanently deletes a product listing and its associated images.
        /// For administrative purposes (use with caution!).
        /// </summary>
        /// <param name="id">The unique identifier of the product to hard-delete.</param>
        Task HardDeleteProductAsync(Guid id);
    }
}

