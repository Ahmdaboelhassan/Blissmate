using Blessmate.Records;

namespace Blessmate.Services.IServices
{
    public interface IEmailConfirmation
    {
        public Task<ManageResponse> GetEmailConfirmationTokenAsync(int id);
        public Task<bool> ConfrimEmailAsync(int id , string token);
    }
}