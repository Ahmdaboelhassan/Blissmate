
using System.ComponentModel.DataAnnotations;

namespace Blessmate.DTOs
{
    public record class MessageDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int? SenderId { get; set; }
        [Required]
        public int? ReciverId { get; set; }
        public DateTime? SendIn { get; set; } = DateTime.UtcNow;
    }
}