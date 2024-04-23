using Blessmate.DTOs;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Blessmate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        public IPaymentService _paymentService { get; }
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
            
        }

        [HttpPost]
        public IActionResult Payment(MakeCheckout model){
            var checkoutOptions = model
                 with {CancelUrl = "https://google.com" , SuccessUrl = "https://google.com"};

             var result = _paymentService.MakeCheckout(checkoutOptions);

             if(string.IsNullOrEmpty(result))
                return BadRequest();

            return Ok(result);
        }
    }
}