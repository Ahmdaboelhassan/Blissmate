using Blessmate.DTOs;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.SignalR;

namespace Blessmate;

public class MessageHub : Hub
{
    private readonly IMessageService _messageService;
    public MessageHub(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

        var httpContext = Context.GetHttpContext();
        var senderId = httpContext.Request.Query["sender"];
        var reciverId = httpContext.Request.Query["receiver"];
        
        if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(reciverId))
            throw new HubException("Please send senderId or receiverId");

        var groupName = GetGroupName(senderId , reciverId);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        var messagesInChat = await _messageService.GetChatMessagesAsync(int.Parse(senderId), int.Parse(reciverId));

        await Clients.Group(groupName).SendAsync("LoadChat" , messagesInChat);
    }

    public async Task SendMessage(MessageDto msg){
        
        var result = await _messageService.SendMessageAsync(msg);

        if(result is null)
            throw new HubException("error happend while send message");
        
        var groupName = GetGroupName(msg.SenderId.ToString(), msg.ReciverId.ToString());


        await Clients.Group(groupName).SendAsync("  " , msg);

    }

    string GetGroupName(string senderId , string reciverId){
        return string.CompareOrdinal(senderId, reciverId) > 0 
                     ? $"{senderId}-{reciverId}": $"{reciverId}-{senderId}";
    }



}
