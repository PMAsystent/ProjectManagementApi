namespace Infrastructure.Identity.Helpers
{
    public class AppSettings
    {
        public string AuthKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long Expire { get; set; }
    }
}
