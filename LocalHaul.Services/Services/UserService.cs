using Data.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : IUserService
    {
        public Task<bool> ChangeUserRolesAsync(string userId, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUsersForAdminNoQueryFilterAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserProfileAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RestoreUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserProfileAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
