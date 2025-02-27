

using System.Data;
using Dapper;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Application.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IDbConnection _db;

        public RoomTypeService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<RoomType> CreateRoomType(CreateRoomTypeDTO dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", dto.Name);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@PricePerNight", dto.PricePerNight);
            parameters.Add("@Capacity", dto.Capacity);

            var result = await _db.QueryFirstOrDefaultAsync<RoomType>("CreateRoomType", parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }

        public async Task<int> DeleteRoomType(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.ExecuteAsync("DeleteRoomType", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<RoomType> GetRoomTypeById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.QueryFirstOrDefaultAsync<RoomType>("GetRoomTypeById", parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypes()
        {
            var result = await _db.QueryAsync<RoomType>("GetAllRoomTypes", commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<RoomType> UpdateRoomType(UpdateRoomTypeDTO dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", dto.Id);
            parameters.Add("@Name", dto.Name);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@PricePerNight", dto.PricePerNight);
            parameters.Add("@Capacity", dto.Capacity);
            var result = await _db.QueryFirstOrDefaultAsync<RoomType>("UpdateRoomType", parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }
}