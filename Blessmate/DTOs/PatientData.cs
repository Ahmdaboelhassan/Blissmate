namespace Blessmate;

public record class PatientData
{   
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public bool IsMale { get; set; } 
    public string? PhotoUrl { get; set; }    
    public DateTime inTime { get; set; }

    
}
