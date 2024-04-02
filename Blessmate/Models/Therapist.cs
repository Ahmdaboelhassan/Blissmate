using System.ComponentModel.DataAnnotations;

namespace Blessmate.Models
{
    public class Therapist  : ApplicationUser
    {
        [MaxLength(50)]
        public string? ClinicAddress { get; set; }
        [MaxLength(50)]
        public string? ClinicNumber { get; set; }
        [MaxLength(50)]
        public string? TherpistIdentity { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? Speciality { get; set; }
        public string? CertificateUrl { get; set; }
        public bool IdentityConfirmed { get; set; } = false;
        public int YearsExperience { set; get;} = new Random().Next(5,40);
        public int Age {set; get;} = new Random().Next(28,80);
        public int Price {set; get;} = new Random().Next(100,1000);
    }

}