using System.ComponentModel.DataAnnotations;
using Blessmate.DTOs;

namespace Blessmate.Records
{
    public class TherapistRegister : Register
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string ClinicAddress { get; set; }

        [Required]
        public string ClinicNumber { get; set; }
    }
}

