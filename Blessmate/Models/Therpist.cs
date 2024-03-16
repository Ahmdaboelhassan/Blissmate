using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Blessmate.Models
{
    public class Therpist : IdentityUser<int>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string? ClinicAddress { get; set; }
        public string? ClinicNumber { get; set; }
        public string? TherpistIdentity { get; set; }
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? Speciality { get; set; }
        public string? PhotoUrl { get; set; }
        public int YearsExperience { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsMale { get; set; } = true;
    }

}