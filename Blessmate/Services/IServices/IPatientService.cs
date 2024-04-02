using System.Linq.Expressions;
using Blessmate.DTOs;
using Blessmate.Models;

namespace Blessmate;

public interface IPatientService
{
        Task<IEnumerable<TherapistData>> GetTherpists();
        Task<IEnumerable<TherapistData>> GetFiltredTherpists(TherapistFilter filter);
        Task<IEnumerable<TherapistData>> GetTherpistsOrderBy(Expression<Func<Therapist,int>> expression);
        Task<IEnumerable<TherapistData>> GetTherpistsOrderByDesc(Expression<Func<Therapist, int>> expression);
        Task<IEnumerable<TherapistData>> GetTherpistsOrderByAppointment();
        Task<IEnumerable<AppointmentDto>> GetAvailableAppointment(int therapistId);
        Task<bool> MakeAppointment(AppointmentDto dto);
}
