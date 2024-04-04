using System.Security.Claims;
using System.Security.Principal;

namespace TroubleTrails.Extensions
{
    public static class IdentityExtensions  // one instance of this class will be created and shared across all requests
    {
        public static int? GetCompanyId(this IIdentity indentity) // 
        {
            Claim? claim = ((ClaimsIdentity)indentity).FindFirst("CompanyId"); //find the claim with the key "CompanyId"
            // Ternary operator (if/else)
            return (claim != null) ? int.Parse(claim.Value) : null; 
            
            //if the claim is not null, parse the value to an int and return it, otherwise return null
            // Above Ternary operator is equivalent to the following if/else statement:

            // int result;
            //if (claim != null)
            //{
            //    result = int.Parse(claim.Value);
            //}
            //else
            //{
            //    result = 0;
            //} 
            //return result;

        
        }
    }
}
