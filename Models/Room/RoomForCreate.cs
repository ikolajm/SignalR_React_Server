using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoomForCreate
    {

        public string Name { get; set; }
        public string Password { get; set; }
        public bool PassEnabled { get; set; }
        // Relationships
        public int UserId {get; set;}
    }
}