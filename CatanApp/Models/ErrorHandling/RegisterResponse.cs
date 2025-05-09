using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanApp.Models.Accounts;

namespace CatanApp.Models.ErrorHandling
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public AppUserDto? User { get; set; }
    }
}