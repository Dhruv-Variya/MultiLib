using MovieMainService.Dtos.Movie;
using MovieMainService.Dtos.User;
using MovieMainService.models;

namespace MovieMainService.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthUserDto>> Authenticate(string username, string password);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<RegisterUserDto>>> RegisterUser(RegisterUserDto newUser);

    }
}
