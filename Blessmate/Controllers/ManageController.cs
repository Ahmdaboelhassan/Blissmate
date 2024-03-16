using Blessmate.Helpers;
using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Blessmate.Controllers
{
    [Route("[controller]")]
    public class ManageController : ControllerBase
    {
        private readonly IEmailConfirmation _emailConfirmation;
        private readonly IResetPassword _resetPassword;
        private readonly IEmailSender _emailSender;
        public ManageController(IEmailConfirmation emailConfirmation,
         IResetPassword resetPassword, IEmailSender emailSender)
        {
            _emailConfirmation = emailConfirmation;
            _resetPassword = resetPassword;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Route("SendEmailConfirmation/{id}")]
        public async Task<ActionResult> SendEmailConfirmation(int id){

            var response = await _emailConfirmation.GetEmailConfirmationTokenAsync(id);

            if (!response.IsCompleted)
                 return BadRequest(response.Message);
            
            string callBackUrl = HostUrl(Request) + Url.Action(nameof(ConfirmEmail),"Manage",
                new {id , token = response.token});

            await _emailSender.SendEmail(response.email,"Confirm Your Email",HTMLTemplates.ConfirmEmail(callBackUrl));

            return Ok();
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] int id ,[FromQuery] string token ){

            var isConfirmed = await _emailConfirmation.ConfrimEmailAsync(id,token);

            if (!isConfirmed) return BadRequest();
                
            return new ContentResult{
                Content = "<h1>Email Confirmed Successfully</h1>",
                ContentType = "text/html"
            };
        }

        [HttpGet]
        [Route("ResetPasswordToken/{id}")]
        public async Task<IActionResult> ResetPasswordToken(int id){

            var response = await _resetPassword.GetPasswordResetToken(id);

            if (!response.IsCompleted)
                 return BadRequest(response.Message);
            
            return Ok(response);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPassModel model){
            
            if(!ModelState.IsValid)
                return BadRequest("Please Check keys Value");
            
            var response = await _resetPassword.ResetPasswordAsync(model);

            if (!response.IsCompleted) return BadRequest(response.Message);
                
            return Ok(response);
            
        }

        private string HostUrl(HttpRequest req){
            
            string Https = req.IsHttps?  "Https://" : "Http://";

            return Https + req.Host.Value;
        }

    }
}