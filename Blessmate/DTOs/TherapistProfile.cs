using System.ComponentModel.DataAnnotations;

namespace Blessmate;

public class TherapistProfile
{
    [Required]
    public string? Decreption { get; set; }
    [Required]
    public string? Speciality { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}
