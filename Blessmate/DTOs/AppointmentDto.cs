using System.ComponentModel.DataAnnotations;

namespace Blessmate;

public class AppointmentDto
{
        [Required]
        public int? TherpistId {get; set;}
        [Required]
        public string InTime {get;set;}
        [Required]
        public int? PatientId {get; set;}
}
