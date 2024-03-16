using Blessmate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blessmate.Data
{
    public class AppDbContext : IdentityDbContext <Therpist, IdentityRole<int> ,int,
    IdentityUserClaim<int>, IdentityUserRole<int>,IdentityUserLogin<int>,
    IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        public DbSet<Appointment> Appointments { get; set; }
    }
}