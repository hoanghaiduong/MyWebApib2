using System.Text.Json.Serialization;

namespace MyWebApi.Domain.Entities
{
    public class RoomTypes : BaseEntity<int>
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? PricePerHour { get; set; }
        public int? PricePerNight { get; set; }
        public int? SingleBed { get; set; }
        public int? DoubleBed { get; set; }
        public int? Sizes { get; set; }
        public int? Capacity { get; set; }
        public string? Thumbnail { get; set; }
        [JsonIgnore]
        public string? Images { get; set; }// ["image1.jpg", "image2.jpg"]

        [JsonPropertyName("Images")]
        public List<string> ImageList
        {
            get
            {
                if (string.IsNullOrEmpty(Images) || !Images.StartsWith("["))
                {
                    return new List<string>();
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Images);
            }
        }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}