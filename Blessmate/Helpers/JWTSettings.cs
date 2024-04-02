namespace Blessmate.Helpers
{
    public class JWTSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string JWTKey { get; set; }
        public int DurationInDays { get; set; }
    }
}