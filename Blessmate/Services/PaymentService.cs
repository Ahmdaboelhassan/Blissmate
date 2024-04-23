using Blessmate.DTOs;
using Blessmate.Services.IServices;
using Stripe;
using Stripe.Checkout;

namespace Blessmate.Services;

public class PaymentService : IPaymentService
{   
    public ILogger<IPaymentService> _logger { get; }
    public PaymentService(ILogger<IPaymentService> logger)
    {
           _logger = logger;
        
    }
    public string? MakeCheckout(MakeCheckout model)
    {   
        if (model.SessionPrice * model.Times < 50) return null;

        try
         {
            var options = new SessionCreateOptions
            {   
                Mode = "payment",
                SuccessUrl = model.SuccessUrl,
                CancelUrl = model.CancelUrl,
                PaymentMethodTypes = new List<string>{ "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = model.Times ,
                        PriceData = new SessionLineItemPriceDataOptions{
                            Currency = "EGP",
                            UnitAmountDecimal = model.SessionPrice * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Blessmate", 
                                Description = "Pay To Your Awsome Therapist",
                            }
                        }
                    },
                },
    
            };
            var service = new SessionService();
            var session = service.Create(options);
            return session.Url;
         }
         catch (StripeException ex){
            _logger.LogError("Error in Stripe Service");
            _logger.LogError(ex.Message);
         }
         return null;
    }
}
