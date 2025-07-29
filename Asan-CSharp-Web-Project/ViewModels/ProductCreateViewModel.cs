using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static LocalHaul.GlobalCommon.ValidationConstants.Product;
namespace Asan_CSharp_Web_Project.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title must be between {2} and {1} characters.")]
        [Display(Name = "Product Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Description must be between {2} and {1} characters.")]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(PriceMinValue, PriceMaxValue, ErrorMessage = "Price must be between {1} and {2}.")]
        [Display(Name = "Selling Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Populated with SelectListItems for the category dropdown in the form.
        /// </summary>
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Represents the uploaded image file for the product.
        /// </summary>
        [Display(Name = "Product Image")]
        // [Required(ErrorMessage = "An image is required for the product.")] // Uncomment if an image is mandatory for creation
        public IFormFile ImageFile { get; set; }
    }
}
