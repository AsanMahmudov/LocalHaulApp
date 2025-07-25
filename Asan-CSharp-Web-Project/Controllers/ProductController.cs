using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace Asan_CSharp_Web_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager; // To get the current user's details
        private readonly IWebHostEnvironment _webHostEnvironment; // For image file operations

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Displays a list of all active product listings, with options for search and category filtering.
        /// </summary>
        /// <param name="searchTerm">Optional text to search within product titles/descriptions.</param>
        /// <param name="categoryId">Optional category ID to filter products by.</param>
        /// <returns>A view displaying the filtered/searched product listings.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm, Guid? categoryId)
        {
            // Fetch all categories for the filter dropdown
            IEnumerable<Category> categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == categoryId
            });
            ViewBag.SearchTerm = searchTerm; // Pass search term back to view for persistence

            IEnumerable<Product> products;
            if (!string.IsNullOrWhiteSpace(searchTerm) || categoryId.HasValue)
            {
                // Implement search and filter logic in your service
                products = await _productService.SearchAndFilterProductsAsync(searchTerm, categoryId);
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }

            return View(products);
        }

        /// <summary>
        /// Displays the detailed information for a single product listing.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>A view displaying product details or a NotFound result if the product does not exist or is soft-deleted.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null) // Global query filter handles IsDeleted = true
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// Displays the form for creating a new product listing.
        /// Requires user to be logged in.
        /// </summary>
        /// <returns>A view with the product creation form.</returns>
        [HttpGet]
        [Authorize] // Only authenticated users can create products
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var viewModel = new ProductCreateViewModel
            {
                Categories = categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            };
            return View(viewModel);
        }

        /// <summary>
        /// Handles the submission of the new product listing form.
        /// </summary>
        /// <param name="model">The view model containing product data and uploaded image.</param>
        /// <returns>Redirects to product listings on success, or redisplays form with errors.</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] // Prevents Cross-Site Request Forgery attacks
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, re-populate categories and return to view
                model.Categories = (await _categoryService.GetAllCategoriesAsync())
                                    .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                return View(model);
            }

            // Get the current logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create a Product entity from the view model
            var product = new Product
            {
                Id = Guid.NewGuid(), // Generate new GUID for product
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                SellerId = userId,
                PostedDate = DateTime.UtcNow,
                IsDeleted = false // New products are not soft-deleted by default
            };

            // Pass the product entity and the IFormFile to the service for saving
            await _productService.CreateProductAsync(product, model.ImageFile);

            TempData["SuccessMessage"] = "Product created successfully!"; // Optional: Success message
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the form for editing an existing product listing.
        /// Only the product's seller or an administrator can access this.
        /// </summary>
        /// <param name="id">The unique identifier of the product to edit.</param>
        /// <returns>A view with the product edit form, or an Unauthorized/NotFound result.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Authorization check: Only seller or admin can edit
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.SellerId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid(); // Or Unauthorized()
            }

            // Populate view model from product entity
            var categories = await _categoryService.GetAllCategoriesAsync();
            var viewModel = new ProductEditViewModel // Assuming you have a ProductEditViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Categories = categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            };

            return View(viewModel);
        }

        /// <summary>
        /// Handles the submission of the edited product listing form.
        /// Only the product's seller or an administrator can update.
        /// </summary>
        /// <param name="model">The view model containing updated product data and optional new image.</param>
        /// <returns>Redirects to product details on success, or redisplays form with errors.</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await _categoryService.GetAllCategoriesAsync())
                                    .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                return View(model);
            }

            var productToUpdate = await _productService.GetProductByIdAsync(model.Id);
            if (productToUpdate == null)
            {
                return NotFound();
            }

            // Authorization check: Only seller or admin can edit
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (productToUpdate.SellerId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            // Update product properties (excluding SellerId, PostedDate, IsDeleted)
            productToUpdate.Title = model.Title;
            productToUpdate.Description = model.Description;
            productToUpdate.Price = model.Price;
            productToUpdate.CategoryId = model.CategoryId;

            await _productService.UpdateProductAsync(productToUpdate, model.ImageFile);

            TempData["SuccessMessage"] = "Product updated successfully!";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        /// <summary>
        /// Displays a confirmation page for soft-deleting a product listing.
        /// Only the product's seller or an administrator can access this.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>A view for delete confirmation, or an Unauthorized/NotFound result.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Authorization check: Only seller or admin can delete
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.SellerId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            return View(product);
        }

        /// <summary>
        /// Handles the soft-deletion of a product listing.
        /// Only the product's seller or an administrator can perform this action.
        /// </summary>
        /// <param name="id">The unique identifier of the product to soft-delete.</param>
        /// <returns>Redirects to product listings on success, or NotFound/Unauthorized.</returns>
        [HttpPost, ActionName("Delete")] // ActionName to distinguish from GET Delete
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id); // Get product with query filter
            if (product == null)
            {
                return NotFound(); // Product already soft-deleted or doesn't exist
            }

            // Authorization check: Only seller or admin can delete
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.SellerId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            await _productService.SoftDeleteProductAsync(id);

            TempData["SuccessMessage"] = "Product soft-deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
