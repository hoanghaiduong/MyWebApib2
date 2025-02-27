

using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomType> CreateRoomType(CreateRoomTypeDTO dto);
        Task<RoomType> GetRoomTypeById(int id);
        Task<IEnumerable<RoomType>> GetRoomTypes();
        Task<RoomType> UpdateRoomType(UpdateRoomTypeDTO dto);
        Task<int> DeleteRoomType(int id);
    }
}