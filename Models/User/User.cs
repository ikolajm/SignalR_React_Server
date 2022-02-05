using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Avatar  { get; set; }
    }
}