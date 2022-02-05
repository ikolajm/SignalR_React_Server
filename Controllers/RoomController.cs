using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Services.UserService;
using backend.Services.RoomService;
using backend.Models;

namespace backend.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IConfiguration _config;
        public RoomController(IRoomService roomService, IConfiguration config)
        {
            _roomService = roomService;
            _config = config;
        }

        [HttpGet]
        public async Task<object> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRooms();

            return Ok(new { rooms = rooms });
        }

        // Get room and all messages
        [HttpGet("{id}")]
        public async Task<object> GetRoom(int id)
        {
            var roomInfo = await _roomService.GetRoomInformation(id);
            var roomMessages = await _roomService.GetRoomMessages(id);

            return Ok(new { room = roomInfo, messages = roomMessages });
        }

        // Create room
        [HttpPost]
        public async Task<ActionResult<Room>> CreateRoom(RoomForCreate room)
        {
            // == PRE POST CHECKS == 
            // Make sure no given fields are blank
            if (room.Name == "")
            {
                return BadRequest("Please ensure your room has a name");
            }
            // == POST POST ==
            var toCreate = await _roomService.CreateRoom(room);

            return Ok(new { room = toCreate });
        }

        // Edit room
        [HttpPut("{id}")]
        public async Task<object> UpdateRoom(RoomForEdit room)
        {
            var update = await _roomService.EditRoom(room);

            return Ok(new { updatedRoom = update });
        }

        // Delete room
        [HttpPost("delete/{id}")]
        public async Task<object> DeleteRoom(RoomForDelete obj)
        {
            var room = await _roomService.DeleteRoom(obj);

            if (room == "Fail")
            {
                return BadRequest("Could not delete the room");
            }

            return Ok(new { message = "Room deleted" });
        }
    }
}