using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Interfaces
{
    public interface IHotelService
    {
        public Task<List<Hotel>> GetHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);
        public Task<Hotel> CreateHotelAsync(Hotel hotel);
        public Task<Hotel> UpdateHotelAsync(int id, Hotel hotel);
        public Task<int> DeleteHotelAsync(int id);
    }
}