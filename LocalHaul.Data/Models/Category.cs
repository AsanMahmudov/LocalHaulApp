using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a category for product listings.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// This is the primary key for the 'Categories' table.
        /// </summary>
        [Key]
        public Guid Id { get; set; } // Changed to Guid

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of products belonging to this category.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Gets or sets a value indicating whether the category is soft-deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
