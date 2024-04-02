using Blessmate.DTOs;

namespace Blessmate;

public interface IDashbourdService
{
    Task<AppStatictics> GetApplicationStatictics();
    Task<IEnumerable<TherapistData>> GetUnConfirmedTherapists();
    Task<bool> ConfirmTherapist(int therapistId);
    Task<bool> DeleteTherpist(int therapistId);
}
