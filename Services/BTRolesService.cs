using Microsoft.AspNetCore.Identity;
using TroubleTrails.Data;
using TroubleTrails.Models;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Services
{
    public class BTRolesService : IBTRoleService // implement the interface
    {
        private readonly ApplicationDbContext _context; // create a private field for the dbcontext
        private readonly RoleManager<IdentityRole> _roleManager; // create a private field for the role manager
        private readonly UserManager<BTUser> _userManager; // create a private field for the user manager

        // constructor
        public BTRolesService(ApplicationDbContext context,
                               RoleManager<IdentityRole> roleManager,
                               UserManager<BTUser> userManager) 
        { 
            _context = context; // set the dbcontext to the private field
            _roleManager = roleManager; // set the role manager to the private field
            _userManager = userManager; // set the user manager to the private field
        }

        async public Task<bool> AddUserToRoleAsync(BTUser user, string roleName)
        {
           bool result = (await _userManager.AddToRoleAsync(user, roleName)).Succeeded; // add the user to the role and store the result
            return result; 
        }

        public Task<string> GetRoleNameByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(BTUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTUser>> GetUsersInRoleAsync(string roleName, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTUser>> GetUsersNotInRoleAsync(string roleName, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserInRoleAsync(BTUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
