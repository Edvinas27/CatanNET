using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanApp.Models.Accounts;
using CatanApp.Models.ErrorHandling;

namespace CatanApp.Interfaces
{
    public interface IUserRepository
    {
        Task<RegisterResponse> CreateUser(RegisterDto registerDto);
        Task<LoginResponse> LoginUser(LoginDto loginDto);
    }
}