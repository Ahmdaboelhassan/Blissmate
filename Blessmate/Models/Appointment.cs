using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blessmate.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PatientId {get; set;}
        [Required]
        public int TherpistId {get; set;}
        [Required]
        public DateTime Time {get;set;}

        [ForeignKey(nameof(TherpistId))]
        public Therapist Therapist {get; set;}

        [ForeignKey(nameof(PatientId))]
        public Patient Patient {get; set;}

    }
}