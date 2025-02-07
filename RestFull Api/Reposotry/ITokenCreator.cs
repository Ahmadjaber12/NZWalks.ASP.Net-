using Microsoft.AspNetCore.Identity;

namespace RestFull_Api.Reposotry
{
    public interface ITokenCreator
    {
        string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}
