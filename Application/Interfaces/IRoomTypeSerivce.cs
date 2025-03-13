
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Interfaces
{
    public interface IRoomTypeSerivce
    {
        public Task<List<RoomTypes>> GetRoomTypeAsync();
        public Task<RoomTypes> GetRoomTypeByIdAsync(int id);
        public Task<RoomTypes> CreateRoomTypeAsync(CreateRoomTypeDTO RoomType);
        public Task<RoomTypes> UpdateRoomTypeAsync(int id, UpdateRoomTypeDTO RoomType);
        public Task<int> DeleteRoomTypeAsync(int id);
    }
}