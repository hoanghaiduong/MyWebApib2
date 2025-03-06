

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyWebApi.Domain.Entities
{
    public class User : BaseEntity<int>
    {
       
        public string UserName { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        // Navigation property 
        
        public UserProfile? Profile { get; set; }
    }
}