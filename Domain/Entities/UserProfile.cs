using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyWebApi.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Avatar { get; set; }

        // Navigation property 
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public User User { get; set; } = null!;
    }
}