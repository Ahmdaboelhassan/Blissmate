using System.ComponentModel.DataAnnotations;

namespace Blessmate.Records
{
    public class ResetPassModel
    {
        [Required]
        public int id {get; set;}
        [Required]
        public string newPassword {get; set;}
        [Required]
        public string token {get; set;}


    }
}