using AuthServer.Core.Configuration;
using AuthServer.Core.Models;
using AuthServer.Core.Security.Jwt.Concrete;

namespace AuthServer.Core.Services.Abstract
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
        ClientTokenDto CreateTokenForClient(Client client);
    }
}
