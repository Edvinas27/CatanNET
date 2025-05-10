using CatanApp.Models.Accounts;
using CatanApp.Models.ErrorHandling;

namespace CatanApp.Interfaces
{
    public interface IUserRepository
    {
        Task<RegisterResponse> CreateUser(RegisterDto registerDto);
        Task<LoginResponse> LoginUser(LoginDto loginDto);
        Task<List<AppUserDto>> GetAllUsers();
    }
}