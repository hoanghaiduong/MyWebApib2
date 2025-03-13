using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Application.DTO
{
    public class RoomTypeDTO
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
        public string? Images { get; set; }
    }
}