using System.Text;

namespace Blessmate.Records
{
    public class AuthResponse
    {
        public string? messages { get; set; }
        public string? email { get; set; }
        public int? id { get; set; }
        public string? firstname  { get; set; }
        public string? lastname  { get; set; }
        public string? token { get; set; }
        public DateTime expireOn { get; set; }
        public bool isAuth { get; set; }
        public bool isConfirmed { get; set; }
        public bool isEmailConfirmed { get; set; }

    }
}