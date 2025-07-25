using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// Defines the contract for user-related business operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a user's profile information by their user ID.
        /// Respects soft-delete status via global query filter.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The ApplicationUser entity if found, otherwise null.</returns>
        Task<ApplicationUser> GetUserProfileAsync(string userId);

        /// <summary>
        /// Updates a user's profile information.
        /// </summary>
        /// <param name="user">The ApplicationUser entity with updated data.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateUserProfileAsync(ApplicationUser user);

        // Admin-specific methods
        /// <summary>
        /// Retrieves all users, including soft-deleted ones (ignores global query filter).
        /// For administrative purposes.
        /// </summary>
        /// <returns>A collection of all ApplicationUser entities.</returns>
        Task<IEnumerable<ApplicationUser>> GetAllUsersForAdminNoQueryFilterAsync();

        /// <summary>
        /// Changes the roles assigned to a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose roles are to be changed.</param>
        /// <param name="roles">A list of role names to assign to the user.</param>
        /// <returns>True if role change was successful, false otherwise.</returns>
        Task<bool> ChangeUserRolesAsync(string userId, IEnumerable<string> roles);

        /// <summary>
        /// Soft-deletes a user account by setting its IsDeleted flag to true.
        /// </summary>
        /// <param name="userId">The ID of the user to soft-delete.</param>
        Task SoftDeleteUserAsync(string userId);

        /// <summary>
        /// Restores a soft-deleted user account by setting its IsDeleted flag to false.
        /// </summary>
        /// <param name="userId">The ID of the user to restore.</param>
        Task RestoreUserAsync(string userId);
    }
}
