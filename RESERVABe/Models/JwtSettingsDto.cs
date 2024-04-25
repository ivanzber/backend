namespace RESERVABe.Models
{
    public class JwtSettingsDto
    {
        public string IssuerSingingKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int ExpirationTimeMinutes { get; set; }
        public int ExpirationTimeHours { get; set; }
        public bool FlagExpirationTimeMinutes { get; set; }
        public bool FlagExpirationTimeHours { get; set; }
    }
}
