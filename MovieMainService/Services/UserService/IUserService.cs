using MainService.Dtos.User;
using MainService.models;
using MainService.Dtos.Movie;

namespace MainService.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthUserDto>> Authenticate(string username, string password);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<RegisterUserDto>>> RegisterUser(RegisterUserDto newUser);

    }
}
