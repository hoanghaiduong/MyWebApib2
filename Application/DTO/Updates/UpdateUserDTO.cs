
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.DTO.Updates
{
    public class UpdateUserDTO
    {
        public int? HotelId { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? RefreshToken { get; set; }
        public bool? IsDisabled { get; set; } = false;
        public bool? EmailVerified { get; set; } = false;
        public bool? Gender { get; set; } = false;
        public DateTime? LastLogin { get; set; }
    }
}