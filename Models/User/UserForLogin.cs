using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class UserForLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}