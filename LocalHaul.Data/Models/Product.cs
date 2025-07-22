using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a product listing available for sale in the OLXClone application.
    /// </summary>

    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// This is the primary key for the 'Products' table.
        /// </summary>
        [Key]
        public Guid Id { get; set; } // Changed to Guid

        /// <summary>
        /// Gets or sets the title of the product listing.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the detailed description of the product.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product was posted.
        /// Automatically set to UTC (Coordinated Universal Time) now when created.
        /// </summary>
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the foreign key for the Category the product belongs to.
        /// This links to the 'Categories' table.
        /// </summary>
        public Guid CategoryId { get; set; } // Changed to Guid

        /// <summary>
        /// Gets or sets the navigation property to the associated Category entity.
        /// </summary>
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Gets or sets the foreign key for the User (Seller) who posted the product.
        /// This links to the 'AspNetUsers' table (ApplicationUser).
        /// </summary>
        public string SellerId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the navigation property to the ASP.NET Identity User (Seller) entity.
        /// </summary>
        public ApplicationUser Seller { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of images associated with this product.
        /// This represents a one-to-many relationship with the 'Images' table.
        /// </summary>
        public ICollection<Image> Images { get; set; } = new List<Image>();

        /// <summary>
        /// Gets or sets a value indicating whether the product listing is soft-deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }


}