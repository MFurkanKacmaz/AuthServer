using AuthServer.Core.Security.Authentication;
using AuthServer.Core.Security.Jwt.Concrete;
using SharedLibrary.DTOs;

namespace AuthServer.Core.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);
        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
