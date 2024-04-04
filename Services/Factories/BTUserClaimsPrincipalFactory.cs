using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TroubleTrails.Models;

namespace TroubleTrails.Services.Factories
{
    public class BTUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<BTUser, IdentityRole>  //inherits from UserClaimsPrincipalFactory
    {
        //constructor
        public BTUserClaimsPrincipalFactory(UserManager<BTUser> userManager, 
                                            RoleManager<IdentityRole> roleManager, 
                                            IOptions<IdentityOptions> optionsAccessor) 
        : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(BTUser user) //override the GetClaimsAsync method
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user); //call the base method
            identity.AddClaim(new Claim("CompanyId", user.CompanyID.ToString())); //add a claim to the identity object
            return identity;         
        }
    }
}
