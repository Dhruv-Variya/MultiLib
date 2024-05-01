using MultiLib.Dtos.Movie;
using MultiLib.Dtos.User;
using MultiLib.models;

namespace MultiLib.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthUserDto>> Authenticate(string username, string password);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<RegisterUserDto>>> RegisterUser(RegisterUserDto newUser);

    }
}
