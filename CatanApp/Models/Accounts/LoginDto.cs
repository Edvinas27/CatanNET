using System.ComponentModel.DataAnnotations;

namespace CatanApp.Models.Accounts
{
    public class LoginDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}