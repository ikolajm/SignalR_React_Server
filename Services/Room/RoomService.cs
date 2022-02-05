using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly DbSetup _context;
        public RoomService(DbSetup context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.User)
                .Include(r => r.Messages)
                .ThenInclude(m => m.User)
                .ToListAsync();

            return rooms;
        }
        public async Task<Room> GetRoomInformation(int id)
        {
            var roomA = await _context.Rooms
                .Include(r => r.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return roomA;
        }

        public async Task<List<Message>> GetRoomMessages(int id)
        {
            var roomB = await _context.Rooms
                .Include(r => r.Messages)
                .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return roomB.Messages;
        }

        public async Task<Room> GetRoomById(int id)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(x => x.Id == id);

            return room;
        }

        public async Task<Room> CreateRoom(RoomForCreate room)
        {
            var toCreate = new Room();
            toCreate.Name = room.Name;
            toCreate.PassEnabled = room.PassEnabled;
            toCreate.Password = room.Password;
            toCreate.UserId = room.UserId;

            _context.Rooms.Add(toCreate);
            _context.SaveChanges();

            var newRoom = await GetRoomById(toCreate.Id);

            return newRoom;
        }

        // Edit Room
        public async Task<Room> EditRoom(RoomForEdit room)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == room.Id);
            existingRoom.Name = room.Name;
            // existingRoom.PassEnabled = room.PassEnabled;
            // existingRoom.Password = room.Password;
            _context.SaveChanges();

            var newRoom = await GetRoomById(existingRoom.Id);
            return newRoom;
        }

        // Delete Room
        public async Task<object> DeleteRoom(RoomForDelete obj)
        {
            var room = await _context.Rooms
                .Where(r => r.Id == obj.Id)
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return "Fail";
            }
            else
            {
                if (room.UserId != obj.UserId)
                {
                    return "Fail";
                }
                else
                {
                    _context.Rooms.Remove(room);
                    await _context.SaveChangesAsync();

                    return "Success";
                }
            }
        }
    }
}