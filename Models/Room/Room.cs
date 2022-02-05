using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Room
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public bool PassEnabled { get; set; }
        // Relationships
        public int UserId {get; set;}
        public User? User {get; set;}
        public List<Message>? Messages {get; set;}
    }
}