namespace backend.Models
{
    public class UserConnection
    {
        public int UserId {get; set;}
        public User? User {get; set;}
        public string RoomIdString {get; set;}
    }

}