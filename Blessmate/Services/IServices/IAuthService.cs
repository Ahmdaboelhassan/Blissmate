using Blessmate.DTOs;
using Blessmate.Records;

namespace Blessmate.Services.IServices
{
    public interface IAuthService
    {
        public Task<AuthResponse> RegisterAsync(Register model);
        public Task<AuthResponse> RegisterAsync(TherapistRegister model);
        public Task<AuthResponse> LoginAsync (Login model);
    }
}