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
        public int Id { get; set; }
       
        public string UserName { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
    }
}