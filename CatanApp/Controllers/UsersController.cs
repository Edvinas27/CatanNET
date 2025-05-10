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
            var response = await _userRepo.CreateUser(registerDto);

            if(!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.User);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _userRepo.LoginUser(loginDto);

            if(!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.User);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();
            return Ok(users);
        }
    }
}