namespace AuthServer.Core.Security.Jwt.Concrete
{
    public class RefreshToken
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
