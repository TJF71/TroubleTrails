
using Microsoft.AspNetCore.Identity;
using TroubleTrails.Data;
using TroubleTrails.Models;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Services
{
    public class BTRolesService : IBTRolesService // implement the interface
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

        public async Task<string> GetRoleNameByIdAsync(string roleId)
        {
            IdentityRole role = _context.Roles.Find(roleId); // find the role by its id
            string result = await _roleManager.GetRoleNameAsync(role); // get the name of the role
            return result; // return the name of the role
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(BTUser user)
        {
            IEnumerable<string> result = await _userManager.GetRolesAsync(user); // get all the roles the user is in   
            return result; // return the roles
        }

        public async Task<List<BTUser>> GetUsersInRoleAsync(string roleName, int companyId)
        {
            List<BTUser> users = (await _userManager.GetUsersInRoleAsync(roleName)).ToList(); // get all the users in the role
            List<BTUser> result = users.Where(u => u.CompanyID == companyId).ToList(); // filter the users by company
            return result; // return the users
        }

        public async Task<List<BTUser>> GetUsersNotInRoleAsync(string roleName, int companyId)
        {
          List<string> userIds = (await _userManager.GetUsersInRoleAsync(roleName)).Select(u => u.Id).ToList(); // get all the user ids in the role
          List<BTUser> roleUsers = _context.Users.Where(u => !userIds.Contains(u.Id)).ToList(); // get all the users not in the role       
            
          List<BTUser> result = roleUsers.Where(u => u.CompanyID == companyId).ToList(); // filter the users not in role by company
          
          return result; // return the users

        }

        public async Task<bool> IsUserInRoleAsync(BTUser user, string roleName)
        {
            bool result = await _userManager.IsInRoleAsync(user, roleName); // check if the user is in the role
            return result; // return the result
        }

        public async Task<bool> RemoveUserFromRoleAsync(BTUser user, string roleName)
        {
           bool result = (await _userManager.RemoveFromRoleAsync(user, roleName)).Succeeded; // remove the user from the role and store the result
           return result; // return the result
        }

        public async Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles)
        {
            bool result = (await _userManager.RemoveFromRolesAsync(user, roles)).Succeeded; // remove the user from the roles and store the result
            return result; // return the result
        }
    }
}
