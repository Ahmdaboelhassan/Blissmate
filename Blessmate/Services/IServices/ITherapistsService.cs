using System.Linq.Expressions;
using Blessmate.DTOs;
using Blessmate.Models;

namespace Blessmate.Services.IServices
{
    public interface ITherapistsService
    {

        Task<bool> AddTherapistProfile(int therapistId , TherapistProfile profile);
        Task<bool> AddTherapistCertificate(int therapistId, IFormFile certificate);
        Task<bool> AddTherapistAppointment(AppointmentDto dto);
        Task<IEnumerable<AppointmentHistory>> GetAppointmentHistory(int therapistId); 
        Task<IEnumerable<PatientData>> GetTherapistPatient(int therapistId);
        Task<IEnumerable<PatientData>> GetLastTherapistPatient(int therapistId);
    }
}