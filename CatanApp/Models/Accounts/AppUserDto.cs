using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatanApp.Models.Accounts
{
    public class AppUserDto
    {
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}