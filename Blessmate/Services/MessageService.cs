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
                    .OrderBy(m => m.SendIn)
                    .Select( m => new MessageDto{
                        Content = m.Content,
                        SenderId  = m.SenderId,
                        ReciverId = m.ReciverId,
                        SendIn = m.SendIn
                    })
                    .ToListAsync();

        }
     public Task<IEnumerable<ChatLitsItem>> GetActiveChats(int id){
        if(!_db.Users.Any(u => u.Id == id)) 
            return Task.FromResult(Enumerable.Empty<ChatLitsItem>());
        
        var chats = _db.Messages
                .Where(m => m.SenderId == id || m.ReciverId == id)
                .OrderByDescending(m => m.SendIn)
                .Include(m => m.Reciver)
                .Include(m => m.Sender)
                .Select( m => new ChatLitsItem {
                    Id = m.SenderId == id ? m.ReciverId : m.SenderId ,
                    FirstName = m.SenderId == id ? m.Reciver.FirstName : m.Sender.FirstName,
                    LastName = m.SenderId == id ? m.Reciver.LastName : m.Sender.LastName,
                    PhotoUrl = m.SenderId == id ? m.Reciver.PhotoUrl : m.Sender.PhotoUrl,
                    LastMessage = m.Content,
                    SendIn = m.SendIn
                })
                .GroupBy(li => li.Id)
                .Select(m => m.First())
                .AsEnumerable();    

        if (chats is null)
            return Task.FromResult(Enumerable.Empty<ChatLitsItem>());

        chats = chats.OrderByDescending(m => m.SendIn);

        return Task.FromResult(chats);               
     }

    [Obsolete]
     public async Task<IEnumerable<ChatLitsItem>?> GetActiveChats_OldVersion(int id){

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
