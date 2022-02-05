using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Message
    {
        [Required]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        // Relationships
        public int RoomId {get; set;}
        public int UserId {get; set;}
        public User? User {get; set;}
    }
}