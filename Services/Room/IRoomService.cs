using backend.Models;

namespace backend.Services.RoomService
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRooms();
        Task<Room> GetRoomInformation(int id);
        Task<List<Message>> GetRoomMessages(int id);
        Task<Room> GetRoomById(int id);
        Task<Room> CreateRoom(RoomForCreate room);
        Task<Room> EditRoom(RoomForEdit room);
        Task<object> DeleteRoom(RoomForDelete obj);
    }
}