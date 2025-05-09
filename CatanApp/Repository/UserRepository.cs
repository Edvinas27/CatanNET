using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanApp.Data;
using CatanApp.Interfaces;
using CatanApp.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CatanApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserRepository(ApplicationDbContext context,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AppUserDto?> CreateUser(RegisterDto registerDto)
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
                        return new AppUserDto
                        {
                            UserName = user.UserName!,
                            Token = _tokenService.CreateToken(user)
                        };
                    }
                    else
                    {
                        //failed to add to role
                        return null;
                    }
                }
                else
                {
                    //failed to create
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                return null;
            }
        }

        public async Task<AppUserDto?> LoginUser(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);

            if(user is null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);

            if(!result.Succeeded)
            {
                return null;
            }

            return new AppUserDto
            {
                UserName = user.UserName!,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}