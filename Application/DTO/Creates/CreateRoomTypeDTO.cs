
using System.Text.Json.Serialization;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.DTO.Creates
{
    public class CreateRoomTypeDTO : RoomType
    {
        [JsonIgnore]
        public new int Id { get; set; }
    }
}