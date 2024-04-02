using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Blessmate;

public class AppointmentDetails : AppointmentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsCompleted { get; set; }
}
