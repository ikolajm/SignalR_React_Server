using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class MessageForDelete
    {
        [Required]
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}