namespace Blessmate.DTOs
{
    public record class TherapistData
    {   
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? TherpistIdentity { get; set; }
        public string? ClinicAddress { get; set; }
        public string? ClinicNumber { get; set; }
        public string? Speciality { get; set; }
        public int YearsExperience { get; set; }
        public string? PhotoUrl { get; set; } 
        public string? CertificateUrl { get; set; }
        public bool IdentityConfirmed { get; set; }
        public bool IsMale { get; set; }
        public int Age {set; get;}
        public int Price {set; get;} 
    }
}