using Blessmate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blessmate.Data
{
    public class AppDbContext : IdentityDbContext <ApplicationUser, IdentityRole<int> ,int,
    IdentityUserClaim<int>, IdentityUserRole<int>,IdentityUserLogin<int>,
    IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
            builder.Entity<Patient>(entity => { entity.ToTable("Patients"); });
            builder.Entity<Therapist>(entity => { entity.ToTable("Therapists"); });
        }
    }
}