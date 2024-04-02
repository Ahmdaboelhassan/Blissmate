using Blessmate.DTOs;
using Blessmate.Services;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Blessmate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        public IMessageService _messageService { get; }
        public MessageController(IMessageService messageService)
        {
           _messageService = messageService;
            
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(MessageDto msg){
           
           var result =  await _messageService.SendMessageAsync(msg);

           if(result is null) return BadRequest();
           
           return Ok(msg);
        }

        [HttpGet]
        [Route("ActiveChats/{id}")]
         public async Task<IActionResult> ActiveChats(int id){

            var activeChats = await _messageService.GetActiveChats(id);

            if(activeChats is null) return BadRequest();

            return Ok(activeChats);

         }

        [HttpGet]
        [Route("ChatMessagesAsync")]
         public async Task<IActionResult> ChatMessagesAsync(int senderId , int reciverId){

            var chatMsgs = await _messageService.GetChatMessagesAsync(senderId,reciverId);

            if(chatMsgs is null) return BadRequest();

            return Ok(chatMsgs);

         }
    }
}