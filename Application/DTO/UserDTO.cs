using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.DTO
{
    public class UserDTO
    {
        public int? HotelId { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Avatar { get; set; }
        public string? RefreshToken { get; set; }
        public bool? IsDisabled { get; set; }
        public bool? EmailVerified { get; set; }
        public bool? Gender { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}