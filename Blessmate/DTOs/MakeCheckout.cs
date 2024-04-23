using System.ComponentModel.DataAnnotations;

namespace Blessmate.DTOs
{
    public record class MakeCheckout
    {
        public string? SuccessUrl { get; set; }
        public string? CancelUrl { get; set; }
        [Required]
        public int?  SessionPrice { get; set; }
        [Required]
        public int?  Times { get; set; }
        
    }
}