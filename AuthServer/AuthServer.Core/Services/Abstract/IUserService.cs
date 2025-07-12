using AuthServer.Core.DTOs;
using SharedLibrary.DTOs;

namespace AuthServer.Core.Services.Abstract
{
    public interface IUserService
    {
        Task <Response<UserDto>> CreateUserAsync (CreateUserDto createUserDto);
        Task <Response<UserDto>> GetUserByNameAsync (string username);
    }
}
