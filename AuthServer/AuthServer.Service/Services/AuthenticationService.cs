using AuthServer.Core.Configuration;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories.Abstract;
using AuthServer.Core.Security.Authentication;
using AuthServer.Core.Security.Jwt.Concrete;
using AuthServer.Core.Services.Abstract;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.DTOs;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;

        public AuthenticationService(IOptions<List<Client>> clients, ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericRepository<RefreshToken> refreshTokenRepository)
        {
            _clients = clients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Response<TokenDto>.Fail("Email veya şifre yanlış!", 400, true);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Response<TokenDto>.Fail("Email veya şifre yanlış!", 400, true);
            }

            var token = _tokenService.CreateToken(user);
            var userRefreshToken = _refreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefault();

            if (userRefreshToken == null)
            {
                await _refreshTokenRepository.AddAsync(new RefreshToken
                {
                    UserId = user.Id,
                    Token = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.SaveChangesAsync();
            return Response<TokenDto>.Success(token, 200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x=>x.Id==clientLoginDto.ClientId && x.Secret==clientLoginDto.ClientSecret);
            if (client == null) return Response<ClientTokenDto>.Fail("Geçersiz kullanıcı!",404,true);

            var token=_tokenService.CreateTokenForClient(client);
            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _refreshTokenRepository.Where(x => x.Token == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null) return Response<TokenDto>.Fail("Refresh Token yok!", 404, true);

            var user= await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null) return Response<TokenDto>.Fail("Geçersiz kullanıcı!",404,true);

            var tokenDto=_tokenService.CreateToken(user);

            existRefreshToken.Token=tokenDto.RefreshToken;
            existRefreshToken.Expiration=tokenDto.RefreshTokenExpiration;

            await _unitOfWork.SaveChangesAsync();

            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public async Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken=await _refreshTokenRepository.Where(x=>x.Token== refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null) return Response<NoDataDto>.Fail("Refresh token yok!", 404, true);

            _refreshTokenRepository.Delete(existRefreshToken);

            await _unitOfWork.SaveChangesAsync();
            return Response<NoDataDto>.Success(200);
        }
    }
}
