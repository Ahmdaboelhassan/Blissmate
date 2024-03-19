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
        public byte? YearsExperience { get; set; }
        public bool IdentityConfirmed { get; set; } = false;
    }

}