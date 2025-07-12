namespace AuthServer.Core.Security.Jwt.Concrete
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
