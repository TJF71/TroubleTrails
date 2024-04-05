using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TroubleTrails.Extensions;
using TroubleTrails.Models;
using TroubleTrails.Models.ViewModels;
using TroubleTrails.Services.Interfaces;

namespace TroubleTrails.Controllers
{
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
            return View();
        }
    }
}
