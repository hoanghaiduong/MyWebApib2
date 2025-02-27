

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyWebApi.Domain.Entities
{
    public class RoomType
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Vui điền tên , tên không được để trống")]
        public required string Name { get; set; }
        public string? Description { get; set; }

        public decimal PricePerNight { get; set; }
        [Range(0, 4, ErrorMessage = "Chỉ chấp nhận các giá trị từ 0 -> 4")]
        public int Capacity { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

    }
}