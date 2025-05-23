using System.ComponentModel.DataAnnotations;

namespace CatanApp.Models.Accounts
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}