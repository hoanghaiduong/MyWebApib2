

using System.Text.Json.Serialization;

namespace MyWebApi.Domain.Entities
{
    public class Hotel : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Location { get; set; } = null!;
        public string Descriptions { get; set; } = null!;
        public string? Notes { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public int? Quantity { get; set; }
        public int? Stars { get; set; }
        public string? Thumbnail { get; set; }
        [JsonIgnore]
        public string? Images { get; set; }// ["image1.jpg", "image2.jpg"]
        public int Floor { get; set; }
        public DateTime? CheckInTime { get; set; } = DateTime.Now;
        public DateTime CheckOutTime { get; set; } = DateTime.Now;
        [JsonPropertyName("Images")]
        public List<string> ImageList
        {
            get
            {
                if (string.IsNullOrEmpty(Images) || !Images.StartsWith("["))
                {
                    return [];
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Images);
            }
        }

        public virtual List<RoomTypes> RoomTypes { get; set; } = [];
    }
}