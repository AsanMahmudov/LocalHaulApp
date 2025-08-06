using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static LocalHaul.GlobalCommon.ValidationConstants.Product;

namespace Asan_CSharp_Web_Project.ViewModels
{
    public class ProductEditViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// This is required to identify which product is being edited.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product title.
        /// [Required] ensures the title cannot be empty.
        /// [StringLength] constrains the title's length using constants.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title must be between {2} and {1} characters.")]
        [Display(Name = "Product Title")]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product description.
        /// [Required] ensures the description cannot be empty.
        /// [StringLength] constrains the description's length using constants.
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Description must be between {2} and {1} characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product price.
        /// [Required] and [Range] ensure the price is a valid positive value using constants.
        /// </summary>
        [Required(ErrorMessage = "Price is required.")]
        [Range(PriceMinValue, PriceMaxValue, ErrorMessage = "Price must be a positive number.")]
        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")] // Formats for display as currency
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the category the product belongs to.
        /// </summary>
        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the list of categories for the dropdown select element.
        /// This is populated in the controller's GET action.
        /// </summary>
        public IEnumerable<Category>? Categories { get; set; }

        [Display(Name = "Product Images")]
		public ICollection<IFormFile>? ImageFiles { get; set; } // Changed to ICollection<IFormFile>
	}
}
