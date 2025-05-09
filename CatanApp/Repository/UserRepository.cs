using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanApp.Data;
using CatanApp.Interfaces;
using CatanApp.Models.Accounts;
using CatanApp.Models.ErrorHandling;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CatanApp.Repository
{
    public class UserRepository : IUserRepository
    {        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserRepository(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<RegisterResponse> CreateUser(RegisterDto registerDto)
        {
            try
            {
                var user = new AppUser
                {
                    UserName = registerDto.Username
                };

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password!);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if(roleResult.Succeeded)
                    {
                        return new RegisterResponse
                        {
                            Success = true,
                            User = new AppUserDto
                            {
                                UserName = user.UserName!,
                                Token = _tokenService.CreateToken(user)
                            }
                        };
                    }
                    else
                    {
                        return new RegisterResponse
                        {
                            Success = false,
                            ErrorMessage = "Failed to assign role to user."
                        };
                    }
                }
                else
                {
                    return new RegisterResponse
                    {
                        Success = false,
                        ErrorMessage = "Failed to create user."
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<LoginResponse> LoginUser(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);

            if(user is null)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password."
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);

            if(!result.Succeeded)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password."
                };
            }

            return new LoginResponse
            {
                Success = true,
                User = new AppUserDto
                {
                    UserName = user.UserName!,
                    Token = _tokenService.CreateToken(user)
                }
            };
        }
    }
}