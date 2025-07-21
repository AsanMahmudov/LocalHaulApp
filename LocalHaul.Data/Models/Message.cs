using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Represents a message exchanged between users regarding a product.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the unique identifier for the message.
        /// </summary>
        [Key]
        public Guid Id { get; set; } // Changed to Guid

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the message was sent.
        /// </summary>
        public DateTime SentDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the foreign key for the sender of the message.
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the sender (ApplicationUser) entity.
        /// </summary>
        public ApplicationUser Sender { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the receiver of the message.
        /// </summary>
        public string ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the receiver (ApplicationUser) entity.
        /// </summary>
        public ApplicationUser Receiver { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the product this message is related to.
        /// </summary>
        public Guid ProductId { get; set; } // Changed to Guid

        /// <summary>
        /// Gets or sets the navigation property to the associated Product entity.
        /// </summary>
        public Product Product { get; set; }
    }

}
