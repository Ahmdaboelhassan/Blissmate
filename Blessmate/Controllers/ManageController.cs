using Blessmate.DTOs;
using Blessmate.Helpers;
using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Blessmate.Controllers
{
    [Route("[controller]")]
    public class ManageController : Controller
    {
        private readonly IEmailConfirmation _emailConfirmation;
        private readonly IResetPassword _resetPassword;
        private readonly IEmailSender _emailSender;
        private readonly IPaymentService _paymentService;
        public ManageController(IEmailConfirmation emailConfirmation,
         IResetPassword resetPassword, IEmailSender emailSender , IPaymentService paymentService)
        {
            _emailConfirmation = emailConfirmation;
            _resetPassword = resetPassword;
            _emailSender = emailSender;
            _paymentService = paymentService;
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
        
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ChanagePasswordModel model){
            
            if(!ModelState.IsValid)
                return BadRequest("Please Check keys Value");
            
            var response = await _resetPassword.ChangePassword(model);

            if (!response.IsCompleted) return BadRequest(response.Message);
                
            return Ok(response);
            
        }

        [HttpPost]
        [Route("CheckOut")]
        public async Task<ActionResult> CheckOut([FromBody] MakeCheckout dto){
            
            var successUrl = HostUrl(Request) + Url.Action(nameof(SuccessCheckOut) , "Manage");
            var failedCheckOut = HostUrl(Request) + Url.Action(nameof(FailedCheckOut) , "Manage");

            var model = dto with {SuccessUrl = successUrl , CancelUrl = failedCheckOut};

            var paymentUrl = _paymentService.MakeCheckout(model);
            
            if (string.IsNullOrEmpty(paymentUrl))
                return BadRequest();

            return Ok(paymentUrl);
        }

        [HttpGet]
        [Route("SuccessCheckOut")]
        public async Task<ActionResult> SuccessCheckOut(){
            var template = System.IO.File
                .ReadAllText($"{Directory.GetCurrentDirectory()}/wwwroot/Html/PaymentConfirmation.html")
                .Replace("<p>[Text]</p>", "<h3 style=\"color:green\"> Success CheckOut</h3>");

            return Content(template, "text/html" );
        }
        [HttpGet]
        [Route("FailedCheckOut")]
        public async Task<ActionResult> FailedCheckOut(){
            var template = System.IO.File
                .ReadAllText($"{Directory.GetCurrentDirectory()}/wwwroot/Html/PaymentConfirmation.html")
                .Replace("<p>[Text]</p>", "<h3 style=\"color:red\"> Failed To Make CheckOut</h3>");

            return Content(template , "text/html" );
        }
        private string HostUrl(HttpRequest req){
            
            string Https = req.IsHttps?  "https://" : "http://";

            return Https + req.Host.Value;
        }


    }
}