using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class MessageForCreate
    {
        public string Content { get; set; }
        // Relationships
        public int RoomId {get; set;}
        public int UserId {get; set;}
    }
}