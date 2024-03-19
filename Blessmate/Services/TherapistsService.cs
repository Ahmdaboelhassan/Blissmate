using Blessmate.Data;
using Blessmate.Models;
using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blessmate.Services;

public class TherapistsService : ITherapistsService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public TherapistsService(AppDbContext db , UserManager<ApplicationUser> userManager) {
        _userManager = userManager;
        _db = db;
    }

    public async Task<IEnumerable<AuthResponse>> GetTherpists()
    {  
           var therpists =  _userManager.Users.Select(th => new AuthResponse {

            id = th.Id,
            email = th.Email,
            firstname = th.FirstName,
            lastname = th.LastName,
            isAuth = true
         }) ;

        return await therpists.ToListAsync();
    }
}
