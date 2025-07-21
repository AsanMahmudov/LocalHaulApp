using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// Extends the default IdentityUser to add custom properties for our application's users.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the city where the user is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the collection of products posted by this user.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Gets or sets the collection of messages sent by this user.
        /// </summary>
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();

        /// <summary>
        /// Gets or sets the collection of messages received by this user.
        /// </summary>
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();

        /// <summary>
        /// Gets or sets a value indicating whether the user account is soft-deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
