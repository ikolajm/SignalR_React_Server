using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Services.UserService;
using backend.Services.RoomService;
using backend.Services.MessageService;
using backend.Models;

namespace backend.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IConfiguration _config;
        public MessageController(IMessageService messageService, IConfiguration config)
        {
            _messageService = messageService;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> CreateMessage(MessageForCreate message)
        {
            // == PRE POST CHECKS == 
            // Make sure no given fields are blank
            if (message.Content == "")
            {
                return BadRequest("Please ensure your Message has content");
            }
            // == POST POST ==
            var toCreate = await _messageService.CreateMessage(message);

            return Ok(new { message = toCreate });
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteMessage(MessageForDelete obj)
        {
            var message = await _messageService.DeleteMessage(obj);

            if (message == "Fail")
            {
                return BadRequest("Could not delete the message");
            }

            return Ok(new { message = "Message deleted" });
        }
    }
}