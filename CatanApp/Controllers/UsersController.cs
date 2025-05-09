using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanApp.Data;
using CatanApp.Interfaces;
using CatanApp.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace CatanApp.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        public UsersController(ApplicationDbContext context, IUserRepository userRepo, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userRepo = userRepo;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _userRepo.CreateUser(registerDto);

            if(user is null)
            {
                return BadRequest("Failed to register user");
            }

            return Ok(new AppUserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(new AppUser
                {
                    UserName = user.UserName
                })
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepo.LoginUser(loginDto);

            if(user is null)
            {
                return BadRequest("Failed to login user");
            }

            return Ok(new AppUserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(new AppUser
                {
                    UserName = user.UserName
                })
            });
        }
    }
}