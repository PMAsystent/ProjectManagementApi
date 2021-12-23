namespace Infrastructure.Identity.Helpers
{
    public class AuthSettings
    {
        public string AuthKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string LogoutToken { get; set; }
        public long Expire { get; set; }
    }
}
