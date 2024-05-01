using _NET.Dtos.Movie;
using _NET.Dtos.User;
using _NET.models;

namespace _NET.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthUserDto>> Authenticate(string username, string password);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<RegisterUserDto>>> RegisterUser(RegisterUserDto newUser);

    }
}
