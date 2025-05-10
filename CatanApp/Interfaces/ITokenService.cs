using CatanApp.Models.Accounts;

namespace CatanApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}