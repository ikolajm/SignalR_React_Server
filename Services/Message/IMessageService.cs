using backend.Models;

namespace backend.Services.MessageService
{
    public interface IMessageService
    {
        Task<Message> CreateMessage(MessageForCreate message);
        Task<object> DeleteMessage(MessageForDelete obj);
    }
}