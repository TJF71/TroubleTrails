using TroubleTrails.Models;

namespace TroubleTrails.Services.Interfaces
{
    public interface IBTRoleService
    {
        public Task<bool> IsUserInRoleAsync(BTUser user, string roleName); // first method to check if a user is in a role

        public Task<IEnumerable<string>> GetUserRolesAsync(BTUser user); // second method to list all the roles a user is in

        public Task<bool> AddUserToRoleAsync(BTUser user, string roleName); // third method to add a user to a role

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string roleName); // fourth method to remove a user from a role

        public Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles); // fifth method to remove a user from multiple roles


        // multi-tenant methods below
        public Task<List<BTUser>> GetUsersInRoleAsync(string roleName, int companyId); // sixth method to get all users in a role
        public Task<List<BTUser>> GetUsersNotInRoleAsync(string roleName, int companyId); // seventh method to get all users not in a role

        public Task<string> GetRoleNameByIdAsync(string roleId); // eighth method to get the name of a role by its id


    }
}
