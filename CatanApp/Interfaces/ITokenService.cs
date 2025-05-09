using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatanApp.Models.Accounts;

namespace CatanApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}