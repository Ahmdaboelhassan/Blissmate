using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blessmate.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string PatientId {get; set;}
        [Required]
        public int TherpistId {get; set;}
        [Required]
        public DateTime InTime {get;set;}

        [ForeignKey(nameof(TherpistId))]
        public Therpist Therpist {get; set;}

    }
}