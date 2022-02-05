using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly DbSetup _context;
        public MessageService(DbSetup context)
        {
            _context = context;
        }

        public async Task<Message> GetMessageById(int id)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(x => x.Id == id);

            return message;
        }
        public async Task<Message> GetMessageByIdWithUser(int id)
        {
            var message = await _context.Messages
                .Include(m => m.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return message;
        }


        public async Task<Message> CreateMessage(MessageForCreate message)
        {
            var toCreate = new Message();
            toCreate.Content = message.Content;
            toCreate.CreatedAt = DateTime.UtcNow;
            // RELATIONSHIP DATA
            // User - author
            toCreate.UserId = message.UserId;
            // Post - parent model
            toCreate.RoomId = message.RoomId;

            _context.Messages.Add(toCreate);
            _context.SaveChanges();

            var newMessage = await GetMessageByIdWithUser(toCreate.Id);

            return toCreate;
        }

        public async Task<object> DeleteMessage(MessageForDelete obj)
        {
            var message = await _context.Messages
                .Where(m => m.Id == obj.Id)
                .FirstOrDefaultAsync();

            if (message == null)
            {
                return "Fail";
            }
            else
            {
                if (message.UserId != obj.UserId)
                {
                    return "Fail";
                }
                else
                {
                    _context.Messages.Remove(message);
                    await _context.SaveChangesAsync();

                    return "Success";
                }
            }
        }
    }
}