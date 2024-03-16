using System.ComponentModel.DataAnnotations;

namespace Blessmate.Records
{
    public class Register
    {
        [Required , StringLength(50)]
        public string FirstName { get; set; }

        [Required ,StringLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string ClinicAddress { get; set; }

        [Required]
        public string ClinicNumber { get; set; }
        
        public bool IsMale { get; set; } = true;
    }
}

