using MultiLib.Auth.Dtos.User;
using MultiLib.Auth.models;


namespace MultiLib.Auth.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthUserDto>> Authenticate(string username, string password);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<RegisterUserDto>>> RegisterUser(RegisterUserDto newUser);

    }
}
