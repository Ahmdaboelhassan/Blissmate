using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Blessmate.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Blessmate.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public bool IsMale { get; set; } = true; 
        public string? PhotoUrl { get; set; }    

        [JsonIgnore]
        public IEnumerable<Message> MessagesSent { get; set; }
        [JsonIgnore]
        public IEnumerable<Message> MessagesRecieved { get; set; }
    }
}