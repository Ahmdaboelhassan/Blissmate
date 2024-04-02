using Blessmate.Data;
using Blessmate.DTOs;
using Blessmate.Models;
using Blessmate.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Blessmate.Services
{
    public class MessageService : IMessageService
    {
        public AppDbContext _db { get;}

        public MessageService(AppDbContext db) =>  _db = db;  
        
        public async Task<MessageDto?> SendMessageAsync(MessageDto msg){

            var Sender = _db.Users.FirstOrDefault(u => u.Id == msg.SenderId);
            if(Sender is null  )  return null;

            var reciver  = _db.Users.FirstOrDefault(u => u.Id == msg.ReciverId);
            if(reciver is null )  return null;

            var message = new Message {
                Id = Guid.NewGuid().ToString(),
                Content = msg.Content,
                SendIn = DateTime.UtcNow,
                ReciverId = (int) msg.ReciverId,
                SenderId = (int) msg.SenderId
            };  

            _db.Messages.Add(message);
            if (_db.SaveChanges() > 0 ) return msg;
            
            return null;
      }

        public async Task<IEnumerable<MessageDto>> GetChatMessagesAsync(int senderId , int reciverId){

           return await _db.Messages
                    .Where(msg => 
                        msg.SenderId == senderId && msg.ReciverId == reciverId  ||
                        msg.SenderId == reciverId && msg.ReciverId == senderId )
                    .Select( m => new MessageDto{
                        Content = m.Content,
                        SenderId  = m.SenderId,
                        ReciverId = m.ReciverId,
                        SendIn = m.SendIn
                    })
                    .OrderBy(m => m.SendIn)
                    .ToListAsync();

        }
      
     
      public async Task<IEnumerable<ChatLitsItem>?> GetActiveChats(int id){

        var user = _db.Users.FirstOrDefault(u => u.Id == id);

        if(user is null) return null;

        var recivers = _db.Messages
                .Where(m => m.SenderId == id)
                .OrderByDescending(x => x.SendIn)
                .Include(m => m.Reciver)
                .Select( m => new ChatLitsItem {
                        Id = m.Reciver.Id,
                        FirstName = m.Reciver.FirstName,
                        LastName = m.Reciver.LastName,
                        PhotoUrl = m.Reciver.PhotoUrl,
                        LastMessage = m.Content,
                        SendIn = m.SendIn
                    }).AsEnumerable();
                 
        var senders = _db.Messages
                    .Where(m => m.ReciverId == id)
                    .OrderByDescending(x => x.SendIn)
                    .Include(m => m.Sender)
                    .Select( m => new ChatLitsItem{
                        Id = m.Sender.Id,
                        FirstName = m.Sender.FirstName,
                        LastName = m.Sender.LastName,
                        PhotoUrl = m.Sender.PhotoUrl,
                        LastMessage = m.Content,
                        SendIn = m.SendIn
                    }).AsEnumerable();
                                              
        return recivers.UnionBy(senders, x => x.Id);
      }

    }
}
