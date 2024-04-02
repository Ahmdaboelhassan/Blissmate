using System.Security.AccessControl;

namespace Blessmate;

public class AppointmentHistory
{
    public DateOnly Day { get; set; }
    public IEnumerable<AppointmentDetails> Appointments { get; set; }
}
