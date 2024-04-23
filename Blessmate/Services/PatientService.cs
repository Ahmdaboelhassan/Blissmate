using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blessmate.Data;
using Blessmate.DTOs;
using Blessmate.Models;
using Microsoft.EntityFrameworkCore;

namespace Blessmate;

public class PatientService : IPatientService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    public PatientService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TherapistData>> GetTherpists()
    {
       return await _db.Therapists
                .Where(th => th.IdentityConfirmed)
                .ProjectTo<TherapistData>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
    public async Task<IEnumerable<TherapistData>> GetFiltredTherpists(TherapistFilter filter)
    {
        var query = _db.Therapists.Where(th => th.IdentityConfirmed);
      
        if (filter.IsMale is not null)
        {
            query = query.Where(th => th.IsMale == filter.IsMale.Value);
        }

        if (filter.PriceFrom is not null || filter.PriceTo is not null)
            query = query.Where(th => th.Price >= filter.PriceFrom && th.Price <= filter.PriceTo);

        return await query.ProjectTo<TherapistData>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<IEnumerable<TherapistData>> GetTherpistsOrderBy(Expression<Func<Therapist, int>> expression)
    {   
       return await _db.Therapists
                .Where(th => th.IdentityConfirmed)
                .OrderBy(expression)
                .ProjectTo<TherapistData>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
    public async Task<IEnumerable<TherapistData>> GetTherpistsOrderByDesc(Expression<Func<Therapist, int>> expression)
    {   
       return await _db.Therapists
                .Where(th => th.IdentityConfirmed)
                .OrderByDescending(expression)
                .ProjectTo<TherapistData>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
    public async Task<IEnumerable<AppointmentDto>> GetAvailableAppointment(int therapistId)
    {   
        if(therapistId == 0)
            return Enumerable.Empty<AppointmentDto>();

        return await _db.Appointments
                .Where(ap => ap.InTime > DateTime.Now && ap.TherpistId == therapistId && ap.PatientId == null)
                .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
    public async Task<bool> MakeAppointment(AppointmentDto dto)
    {
        DateTime inTime;
        if(!DateTime.TryParse(dto.InTime , out inTime))  
            return false;

        // this line because we use postgress and its not alllowed another format expect utc!
        inTime =  DateTime.SpecifyKind(inTime , DateTimeKind.Utc);

        if(inTime < DateTime.UtcNow)
            return false;

        if(!_db.Therapists.Any(th => th.Id == dto.TherpistId))
            return false;

       if(_db.Appointments.Any(ap => ap.InTime == inTime && ap.TherpistId == dto.TherpistId))
            return false;

        var newAppoinment = new Appointment{
            InTime = inTime,
            PatientId = dto.PatientId,
            TherpistId = (int)dto.TherpistId,
        };

        _db.Appointments.Add(newAppoinment);
        _db.SaveChanges();
        return true;
    }
    public Task<IEnumerable<TherapistData>> GetTherpistsOrderByAppointment()
    {
       return Task.FromResult( _db.Appointments
                .Where(ap => ap.InTime > DateTime.Now && ap.PatientId == null)
                .Include(ap => ap.Therapist)
                .OrderBy(ap => ap.InTime)
                .ProjectTo<TherapistData>(_mapper.ConfigurationProvider)
                .AsEnumerable()
                .DistinctBy(ap => ap.Id));
            
    }
}
