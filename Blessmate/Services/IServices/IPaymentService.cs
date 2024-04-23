using Blessmate.DTOs;

namespace Blessmate.Services.IServices
{
    public interface IPaymentService
    {
        public string? MakeCheckout(MakeCheckout model);
    }
}