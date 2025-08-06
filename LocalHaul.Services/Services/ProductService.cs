
using Microsoft.AspNetCore.Http; // For IFormFile (for image uploads)
using Microsoft.AspNetCore.Hosting; // Required for IWebHostEnvironment (to get wwwroot path)
using System; // For Guid, DateTime
using System.Collections.Generic;
using System.IO; // For Path, FileStream, Directory
using System.Linq; // For LINQ queries
using System.Threading.Tasks;
using Services.Interfaces;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
	public class ProductService : IProductService
	{
		private readonly LocalHaulDbContext _dbContext;
		private readonly IWebHostEnvironment _webHostEnvironment; // Used to access wwwroot path for image storage

		public ProductService(LocalHaulDbContext dbContext, IWebHostEnvironment webHostEnvironment)
		{
			_dbContext = dbContext;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<IEnumerable<Product>> GetAllProductsAsync()
		{
			return await _dbContext.Products
								   .Include(p => p.Images)
								   .Include(p => p.Category) 
								   .Include(p => p.Seller)   
								   .ToListAsync();
		}
		public async Task<Product> GetProductByIdAsync(Guid id)
		{

			return await _dbContext.Products
								   .Include(p => p.Images) 
								   .Include(p => p.Category) 
								   .Include(p => p.Seller)   
								   .FirstOrDefaultAsync(p => p.Id == id);
		}
		public async Task CreateProductAsync(Product product, IFormFile imageFile)
		{
			_dbContext.Products.Add(product); // Add product to context first

			if (imageFile != null && imageFile.Length > 0)
			{
				// Define the folder path within wwwroot where images will be stored
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

				// Ensure the directory exists, create if it doesn't
				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}
				// Generate a unique filename to prevent conflicts and ensure unique URLs
				string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				// Save the file to the file system
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await imageFile.CopyToAsync(fileStream);
				}

				// Create a new Image entity and link it to the product
				var image = new Image
				{
					Id = Guid.NewGuid(),
					ImagePath = $"/images/products/{uniqueFileName}", // Store relative URL path
					ProductId = product.Id // Link to the newly created product
				};
				_dbContext.Images.Add(image); // Add the image to the context
			}

			await _dbContext.SaveChangesAsync(); // Save all changes to the database
		}
		public async Task<IEnumerable<Product>> GetAllProductsForAdminNoQueryFilterAsync()
		{
			return await _dbContext.Products
						.IgnoreQueryFilters() // Bypass the IsDeleted filter
						.ToListAsync();
		}
		public async Task SoftDeleteProductAsync(Guid id)
		{
			// FindAsync will retrieve the entity even if it's soft-deleted because it queries by primary key directly
			Product? product = await _dbContext.Products.FindAsync(id);
			if (product != null)
			{
				product.IsDeleted = true; // Mark as deleted
				await _dbContext.SaveChangesAsync();
			}
		}
		public async Task HardDeleteProductAsync(Guid id)
		{
			// Retrieve the product, ignoring soft-delete filter
			Product? product = await _dbContext.Products
										   .IgnoreQueryFilters()
										   .Include(p => p.Images) // Eagerly load images here
										   .FirstOrDefaultAsync(p => p.Id == id);

			if (product != null)
			{
				if (product.Images != null) // Check if images collection is loaded
				{
					foreach (var img in product.Images.ToList())
					{
						string filePath = Path.Combine(_webHostEnvironment.WebRootPath, img.ImagePath.TrimStart('/'));
						if (System.IO.File.Exists(filePath))
						{
							System.IO.File.Delete(filePath);
						}
					}
				}

				_dbContext.Products.Remove(product);
				await _dbContext.SaveChangesAsync();
			}
		}
		public async Task RestoreProductAsync(Guid id)
		{
			var product = await _dbContext.Products.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);
			if (product != null)
			{
				product.IsDeleted = false; // Restore
				_dbContext.Products.Update(product);
				await _dbContext.SaveChangesAsync();
			}
		}
		public async Task<IEnumerable<Product>> SearchAndFilterProductsAsync(string? searchTerm, Guid? categoryId)
		{
			IQueryable<Product> query = _dbContext.Products;

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				string lowerSearchTerm = searchTerm.ToLower();
				query = query.Where(p => p.Title.ToLower().Contains(lowerSearchTerm) ||
										 p.Description.ToLower().Contains(lowerSearchTerm));
			}

			if (categoryId.HasValue && categoryId != Guid.Empty) // Check for Guid.Empty if default value is 000...
			{
				query = query.Where(p => p.CategoryId == categoryId.Value);
			}

			return await query.ToListAsync();
		}
		public async Task UpdateProductAsync(Product product, ICollection<IFormFile>? imageFiles) // Changed parameter to ICollection<IFormFile>
		{
			// Attach the product entity to the context in Modified state.
			// EF Core will track changes to its scalar properties.
			_dbContext.Products.Update(product);

			// Only proceed with image handling if new images are provided
			if (imageFiles != null && imageFiles.Any())
			{
				// Step 1: Remove existing images for this product (both from DB and file system)
				var existingImages = await _dbContext.Images
													 .Where(i => i.ProductId == product.Id)
													 .ToListAsync();
				foreach (var img in existingImages)
				{
					string existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, img.ImagePath.TrimStart('/'));
					if (System.IO.File.Exists(existingFilePath))
					{
						System.IO.File.Delete(existingFilePath); // Delete physical file
					}
				}
				_dbContext.Images.RemoveRange(existingImages); // Remove from DB context

				// Define the folder path within wwwroot where images will be stored
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}

				// Step 2: Save each new image
				foreach (var imageFile in imageFiles)
				{
					if (imageFile != null && imageFile.Length > 0)
					{
						string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
						string filePath = Path.Combine(uploadsFolder, uniqueFileName);

						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							await imageFile.CopyToAsync(fileStream);
						}

						// Step 3: Add the new image entity to the database
						var newImage = new Image
						{
							Id = Guid.NewGuid(),
							ImagePath = $"/images/products/{uniqueFileName}",
							ProductId = product.Id
						};
						_dbContext.Images.Add(newImage);
					}
				}
			}

			await _dbContext.SaveChangesAsync(); // Save all changes
		}
	}
}
