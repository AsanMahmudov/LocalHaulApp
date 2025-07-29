using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asan_CSharp_Web_Project.ViewModels
{
    public class ProductListModel
    {
        /// <summary>
        /// Gets or sets the collection of products to display on the page.
        /// </summary>
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Gets or sets the collection of categories for the filter dropdown.
        /// These are SelectListItem objects, suitable for asp-items.
        /// </summary>
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets the search term entered by the user.
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the ID of the selected category for filtering.
        /// Nullable Guid to represent no category selected.
        /// </summary>
        public Guid? SelectedCategoryId { get; set; }
    }
}
