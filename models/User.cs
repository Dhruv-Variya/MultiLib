using MultiLib.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace MultiLib.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }

    }
}
