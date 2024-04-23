using Blessmate.DTOs;

namespace Blessmate.Services.IServices
{
    public interface IMessageService
    {
       Task<MessageDto?> SendMessageAsync(MessageDto msg);
       Task<IEnumerable<MessageDto>> GetChatMessagesAsync(int senderId , int reciverId);
       Task<IEnumerable<ChatLitsItem>> GetActiveChats(int id);

    }
}