using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents an image associated with a product listing.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets the unique identifier for the image.
        /// </summary>
        [Key]
        public Guid Id { get; set; } // Changed to Guid

        /// <summary>
        /// Gets or sets the file path or URL of the image.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the Product this image belongs to.
        /// </summary>
        public Guid ProductId { get; set; } // Changed to Guid

        /// <summary>
        /// Gets or sets the navigation property to the associated Product entity.
        /// </summary>
        public Product Product { get; set; }
    }
}
