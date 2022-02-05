using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoomForEdit
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
        // public string Password { get; set; }
        // public bool PassEnabled { get; set; }
    }
}