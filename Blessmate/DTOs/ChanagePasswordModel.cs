using System.ComponentModel.DataAnnotations;

namespace Blessmate.DTOs
{
    public class ChanagePasswordModel
    {
        [Required]
        public int? id { get; set; }
        [Required]
        public string? oldPassword { get; set; }
        [Required]
        public string? newPassword { get; set; }
    }
}