using Microsoft.AspNetCore.Identity;

namespace EcommerceWeb.Api.Repositories
{
    public interface ITokenRepository
    {

       string CreateJWTToken(IdentityUser user, List<string> roles);
        //void CreateJWTToken(IdentityUser user, IList<string> list);
    }
}
