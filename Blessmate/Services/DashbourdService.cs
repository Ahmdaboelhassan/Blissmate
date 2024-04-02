using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blessmate.Data;
using Blessmate.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Blessmate;

public class DashbourdService : IDashbourdService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public DashbourdService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> ConfirmTherapist(int therapistId)
    {
        var therapist = await _db.Therapists.FindAsync(therapistId);
        if(therapist is null)
            return false;
        
        if(!therapist.IdentityConfirmed) {

            therapist.IdentityConfirmed = true;
            _db.Therapists.Update(therapist);
           await _db.SaveChangesAsync();
        }
        return true;
    }

    public async Task<bool> DeleteTherpist(int therapistId)
    {
        var therapist = await _db.Therapists.FindAsync(therapistId);
        if(therapist is null)
            return false;

        if(!therapist.IdentityConfirmed) {
            _db.Therapists.Remove(therapist);
           await _db.SaveChangesAsync();
        }
        return true;
    }

    public Task<IEnumerable<TherapistData>> GetUnConfirmedTherapists()
    {
       return Task.FromResult(_db.Therapists
                            .Where(th => !th.IdentityConfirmed)
                            .ProjectTo<TherapistData>(_mapper.ConfigurationProvider)
                            .AsEnumerable());
    }

    public async Task<AppStatictics> GetApplicationStatictics()
    { 
        return  new AppStatictics{
            Messages = await _db.Messages.CountAsync(),
            pateints= await _db.Patients.CountAsync(),
            Therapists =  await  _db.Therapists.CountAsync(),
            Users= await _db.Users.CountAsync()
        };
    }

   
}
