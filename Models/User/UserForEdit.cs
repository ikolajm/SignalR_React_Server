using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class UserForEdit
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}