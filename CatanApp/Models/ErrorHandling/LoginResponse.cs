using CatanApp.Models.Accounts;

namespace CatanApp.Models.ErrorHandling
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public AppUserDto? User { get; set; }
    }
}