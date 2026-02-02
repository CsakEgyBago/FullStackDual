using Eszi.Demo.Database.Models;

namespace Dual.Server.Dtos
{
    public class UserDto
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
