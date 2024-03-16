using Blessmate.Models;
using Blessmate.Records;

namespace Blessmate.Services.IServices
{
    public interface ITherapistsService
    {
        Task<IEnumerable<AuthResponse>> GetTherpists();
        
    }
}