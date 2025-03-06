using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Application.DTO
{
    public class UserProfileDTO
    {
        public int? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Avatar { get; set; }
    }
}