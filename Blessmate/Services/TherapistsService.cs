using AutoMapper;
using Blessmate.Data;
using Blessmate.Models;
using Blessmate.Services.IServices;
using Microsoft.EntityFrameworkCore;


namespace Blessmate.Services;

public class TherapistsService : ITherapistsService
{
    private readonly AppDbContext _db;
    private readonly IPhotoService _photoService;
    public TherapistsService(AppDbContext db , IMapper mapper ,
    IPhotoService photoService ) {

        _db = db;
        _photoService =photoService;
    }

     public async Task<bool> AddTherapistAppointment(AppointmentDto dto)
     {
          var therapist = await _db.Therapists.FindAsync(dto.TherpistId);
          if (therapist is null)
               return false;

          DateTime inTime;
          try
          {
               inTime = DateTime.Parse(dto.InTime);
          }
          catch (Exception)
          {
               return false;
          }

          if(inTime < DateTime.UtcNow)
               return false;
          
          var appointmentAvailble = _db.Appointments
               .FirstOrDefault(ap => ap.TherpistId == dto.TherpistId && ap.InTime == inTime);
          
          if(appointmentAvailble is not null)
               return false;

          var appointment = new Appointment(){
               TherpistId = (int)dto.TherpistId,
               InTime = inTime,
               PatientId = dto.PatientId
          };

          _db.Appointments.Add(appointment);
          _db.SaveChanges();
          return true;
     }
     public async Task<IEnumerable<AppointmentHistory>> GetAppointmentHistory(int therapistId)
    {
       var therapist = await _db.Therapists.FindAsync(therapistId);
       if (therapist is null)
            return Enumerable.Empty<AppointmentHistory>();
       
         var appointment =  _db.Appointments
               .Where(ap =>  ap.TherpistId == therapistId && ap.PatientId != null && !ap.IsCompleted)
               .Where(ap => ap.InTime.Year == DateTime.UtcNow.Year)
               .Include(ap => ap.Patient)
               .OrderByDescending(ap => ap.InTime)
               .AsNoTracking().AsEnumerable();

         var uniqueDays = appointment.Select(ap => ap.InTime.Date).Distinct();

         var appointmentHistory = new List<AppointmentHistory>();

         foreach ( var day in uniqueDays){
            var history = new AppointmentHistory{
               Day = DateOnly.FromDateTime(day),
               Appointments = appointment
                .Where(ap => ap.InTime.Date == day)
                .OrderBy(ap => ap.InTime)
                .Select(ap =>  new AppointmentDetails 
                    {
                         PatientId = ap.PatientId, 
                         FirstName = ap.Patient.FirstName,
                         LastName = ap.Patient.LastName,
                         IsCompleted = ap.IsCompleted,
                         PhoneNumber = ap.Patient.PhoneNumber,
                         PhotoUrl =  ap.Patient.PhotoUrl,
                         InTime = ap.InTime.ToString(),
                         TherpistId = ap.TherpistId
                    })
            };
            appointmentHistory.Add(history);
         }
           
      return appointmentHistory;
    }
     public async Task<IEnumerable<AppointmentDto>> GetNextAppointments(int therapistId)
    {
       var therapist = await _db.Therapists.FindAsync(therapistId);
       if (therapist is null)
            return Enumerable.Empty<AppointmentDto>();
       
       return  _db.Appointments
               .Where(ap => ap.PatientId != null && ap.TherpistId == therapistId && !ap.IsCompleted)
               .Select(ap =>  new AppointmentDto {PatientId = ap.PatientId , InTime = ap.InTime.ToString() , TherpistId = ap.TherpistId});
    }
     public async Task<bool> AddTherapistProfile(int therapistId , TherapistProfile profile)
    {
       var therapist = _db.Therapists.Find(therapistId);

       if (therapist is null)
            return false;

       var photoUrl = await _photoService.AddProfilePicture(therapistId, profile.ProfilePicture);
       if (photoUrl is not null)
          therapist.PhotoUrl = photoUrl;
       
       therapist.Description = profile.Decreption;
       therapist.Speciality  = profile.Speciality;
       
       _db.Therapists.Update(therapist);
       _db.SaveChanges();
       return true;
    }
     public async Task<bool> AddTherapistCertificate(int therapistId, IFormFile certificate)
    {
        var therapist = _db.Therapists.Find(therapistId);

        if (therapist is null)
         return false;
      
       var certificateUrl = await _photoService.AddTherapistCertificate(therapistId , certificate);
         if(certificateUrl is null)
         return false;

      therapist.CertificateUrl = certificateUrl;
       _db.Therapists.Update(therapist);
       _db.SaveChanges();

      return true;
    }

    public async Task<IEnumerable<PatientData>> GetTherapistPatient(int therapistId)
    {     
        return await _db.Appointments
               .Where(ap => ap.TherpistId == therapistId && ap.PatientId != null)
               .Include(ap => ap.Patient)
               .OrderByDescending(ap => ap.InTime)
               .Select(ap => new PatientData{
                    Id = ap.Patient.Id,
                    FirstName = ap.Patient.FirstName,
                    LastName = ap.Patient.LastName,
                    PhoneNumber = ap.Patient.PhoneNumber,
                    Email = ap.Patient.Email,
                    IsMale = ap.Patient.IsMale,
                    PhotoUrl = ap.Patient.PhotoUrl,                        
               })
               .Distinct()
               .ToListAsync();

    }

    public async Task<IEnumerable<PatientData>> GetLastTherapistPatient(int therapistId)
    {
        return await _db.Appointments
               .Where(ap => ap.TherpistId == therapistId)
               .Include(ap => ap.Patient)
               .OrderByDescending(ap => ap.InTime)
               .Select(ap => new PatientData{
                    Id = ap.Patient.Id,
                    FirstName = ap.Patient.FirstName,
                    LastName = ap.Patient.LastName,
                    PhoneNumber = ap.Patient.PhoneNumber,
                    Email = ap.Patient.Email,
                    IsMale = ap.Patient.IsMale,
                    PhotoUrl = ap.Patient.PhotoUrl,                        
               })
               .Distinct()
               .Take(3)
               .ToListAsync();
    }
}
