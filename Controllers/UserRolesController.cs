using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TroubleTrails.Extensions;
using TroubleTrails.Models;
using TroubleTrails.Models.ViewModels;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Controllers
{
    [Authorize]
    public class UserRolesController : Controller
    {
        private readonly IBTRolesService _rolesService;
        private readonly IBTCompanyInfoService _companyInfoService;
        public UserRolesController(IBTRolesService rolesService, 
                                    IBTCompanyInfoService companyInfoService)
        {
            _rolesService = rolesService;
            _companyInfoService = companyInfoService;
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles()
        {
            // Add an instance of the ViewModel as a list (model)
            List<ManageUserRolesViewModel> model = new();

            // Get CompanyId
            int companyId = User.Identity.GetCompanyId().Value;

            // Get all company Users
            List<BTUser> users = await _companyInfoService.GetAllMemebersAsync(companyId);
 
            // Loop over the users to populate the ViewModel
            // - instantiate ViewModel
            // - use _rolesService
            // - Create multi-selects
           foreach(BTUser user in users)
            {
                ManageUserRolesViewModel viewModel = new();            // Return the model to the View
                viewModel.BTUser = user;
                IEnumerable<string> selected = await _rolesService.GetUserRolesAsync(user);
                viewModel.Roles = new MultiSelectList(await _rolesService.GetRolesAsync(), "Name", "Name", selected);

                model.Add(viewModel);
            }

            // Return the model to the View
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel member)
        {
            // Get the company Id
            int companyId = User.Identity.GetCompanyId().Value;

            // Instantiate the BTUser
            BTUser? btUser = (await _companyInfoService.GetAllMemebersAsync(companyId)).FirstOrDefault(u => u.Id == member.BTUser.Id);

            // Get Roles for the User
            IEnumerable<string> roles = await _rolesService.GetUserRolesAsync(btUser);

            // Grab the selected role
            string userRole = member.SelectedRoles.FirstOrDefault();

            if(!string.IsNullOrEmpty(userRole)) {

                // Remove User from their roles
                if (await _rolesService.RemoveUserFromRolesAsync(btUser, roles))
                {
                    // Add User to the new role
                    await _rolesService.AddUserToRoleAsync(btUser, userRole);
                }
            }

            //Navigate back to the View
            return RedirectToAction(nameof(ManageUserRoles));
        }

    }
}
