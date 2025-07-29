using System.ComponentModel.DataAnnotations;

namespace Asan_CSharp_Web_Project.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Product Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")] // Formats as currency
        public decimal Price { get; set; }

        [Display(Name = "Posted On")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")] // Formats date
        public DateTime PostedDate { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; } // Display category name directly

        [Display(Name = "Seller")]
        public string SellerUserName { get; set; } // Display seller's username (or combined full name)
        public string SellerId { get; set; } // Keep seller ID for authorization checks in view if needed

        /// <summary>
        /// Collection of image paths for the product.
        /// </summary>
        public IEnumerable<string> ImagePaths { get; set; } = new List<string>();

        // You could add more display-specific properties, e.g.:
        // public bool CanEdit { get; set; } // Set in controller based on user roles/ownership
        // public bool CanDelete { get; set; }
    }
}
