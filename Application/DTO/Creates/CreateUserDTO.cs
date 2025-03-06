
using System.Text.Json.Serialization;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.DTO.Creates
{
    public class CreateUserDTO : UserDTO
    {
        [JsonIgnore]
        public new int Id { get; set; }
    }
}