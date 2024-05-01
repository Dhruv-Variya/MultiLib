using MainService.Dtos.Movie;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using MainService.models;
using MainService.Dtos.User;
using MainService.Data;
using MainService.Helper;

namespace MainService.Services.UserService
{
    public class UserService : IUserService
    {
        private static User user = new User();
        //private readonly DataContext _context;

        //public UserService(DataContext context)
        //{

        //    _context = context;
        //}
        private static List<User> users = new List<User> {
            new User{ Username = "test", Password = "test"},
            new User{ Username = "test1", Password = "test1"}
         };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbusers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbusers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return serviceResponse;
        }




        public async Task<ServiceResponse<AuthUserDto>> Authenticate(string username, string password)
        {
            var serviceResponse = new ServiceResponse<AuthUserDto>();
            //var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            // check if user/username is awailable in databaseor not
            var dbuser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (dbuser == null)
            {
                serviceResponse.Message = "User not found";
                serviceResponse.Success = false;
            }
            // varify user password is match with hash password or not
            else if (dbuser != null && PasswordHasher.VerifyPassword(password, dbuser.Password))
            {
                //henerate tocken for user when login
                dbuser.Token = CreateJwt(dbuser);
                var authUserDto = new AuthUserDto
                {
                    Username = dbuser.Username,
                    Password = dbuser.Password,
                    Token = dbuser.Token
                };
                serviceResponse.Data = authUserDto;
                // serviceResponse.Data = _mapper.Map<AuthUserDto>(dbusers);
                serviceResponse.Message = "Login Successfully";
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Message = "username and password are not matching";
                serviceResponse.Success = false;
            }
            //if (dbusers == null)
            //{
            //    serviceResponse.Message = "User not found";
            //    serviceResponse.Success = false;
            //}
            //else
            //{
            //    var authUserDto = new AuthUserDto
            //    {
            //        Username = dbusers.Username,
            //        Password = dbusers.Password
            //    };
            //    serviceResponse.Data = authUserDto;
            //    // serviceResponse.Data = _mapper.Map<AuthUserDto>(dbusers);
            //    serviceResponse.Message = "Login Successfully";
            //    serviceResponse.Success = true;  
            //}

            return serviceResponse;
        }



        public async Task<ServiceResponse<List<RegisterUserDto>>> RegisterUser(RegisterUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<RegisterUserDto>>();
            var user = _mapper.Map<User>(newUser);
            var pass = CheckPasswordStrengt(user.Password);
            if (await CheckUserNameExistAsync(user.Username))
            {
                serviceResponse.Message = "User Name Already Exist";
                serviceResponse.Success = false;
            }
            else if (await CheckEmailExistAsync(user.Email))
            {
                serviceResponse.Message = "Email Registered Already, Register With Other Email";
                serviceResponse.Success = false;
            }
            else if (!string.IsNullOrEmpty(pass))
            {
                serviceResponse.Message = pass.ToString();
                serviceResponse.Success = false;
            }
            else
            {
                user.Password = PasswordHasher.HashPassword(user.Password);
                user.Role = "User";
                user.Token = "";
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                var registeredUsersDto = users.Select(u => _mapper.Map<RegisterUserDto>(u)).ToList();

                serviceResponse.Message = "User Created Successfully";
                serviceResponse.Data = registeredUsersDto;

            }
            return serviceResponse;

        }



        // check user name or email already there in database or not for new registrations
        private async Task<bool> CheckUserNameExistAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);

        }
        private async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);

        }


        // check password strength : length , Alphanumeric and special chars
        private string CheckPasswordStrengt(string password)
        {

            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("minimum 8 characters required" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("password should be Alphanumeric" + Environment.NewLine);
            if (!Regex.IsMatch(password, @"[\W_]"))
                sb.Append("Password should contain special chars" + Environment.NewLine);
            return sb.ToString();
        }






        // jwt tocken generation : claims : user role and full name
        private string CreateJwt(User dbuser)
        {
            var jwtTockenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, dbuser.Role),
                new Claim(ClaimTypes.Name, $"{dbuser.Fname} {dbuser.Lname}")
            });

            var credentals = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tockenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentals
            };

            var token = jwtTockenHandler.CreateToken(tockenDescriptor);


            return jwtTockenHandler.WriteToken(token);
        }


    }

}
