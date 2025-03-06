
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.DTO.Updates
{
    public class UpdateUserDTO : UserDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public UserProfile? Profile { get; set; }
    }
}