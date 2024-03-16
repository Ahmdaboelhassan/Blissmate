namespace Blessmate.Records;

public class TherapistProfile
{
    public string? PhotoUrl { get; set; }
    public string? Description { get; set; }
    public string? Specialty { get; set; }
    public IEnumerable<DateTime>? AvailbleAppointmnet { get; set; }
}