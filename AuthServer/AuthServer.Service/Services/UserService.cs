using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTOs;

namespace AuthServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User { Email = createUserDto.Email, UserName = createUserDto.Username };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<UserDto>.Fail(new ErrorDto(errors, true), 400);
            }

            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), 200);
        }

        public async Task<Response<UserDto>> GetUserByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return Response<UserDto>.Fail("Geçersiz kullanıcı!", 400, true);

            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), 200);
        }
    }
}
