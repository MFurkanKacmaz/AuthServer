namespace AuthServer.Core.Security.Jwt.Concrete
{
    public class TokenDto
    {
        //AccessToken AccessToken { get; set; }
        //RefreshToken RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
