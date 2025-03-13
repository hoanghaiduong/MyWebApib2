using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Application.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Services
{
    public class RoomTypeService : IRoomTypeSerivce
    {
        private readonly IDbConnection _db;

        public RoomTypeService(IDbConnection db)
        {
            _db = db;
        }

        public Task<RoomTypes> CreateRoomTypeAsync(CreateRoomTypeDTO RoomType)
        {
            //  @HotelId INT,
            // @Name NVARCHAR(100),
            // @Description NVARCHAR(500) = NULL,
            // @PricePerNight DECIMAL(18,2),
            // @SingleBed INT=NULL,
            // @DoubleBed INT=NULL,
            // @Capacity INT,
            // @Sizes INT,
            // @Thumbnail NVARCHAR(MAX) = NULL,
            // @Images NVARCHAR(MAX) = NULL,
            // @CreatedAt DATETIME2(7) = NULL,
            // @UpdatedAt DATETIME2(7) = NULL
            try
            {
                // string testImage="https://www.google.com.vn/images/branding/googlelogo/2x/googlelogo_color_92x30dp.png";
                List<string> images =
                ["https://cf.bstatic.com/xdata/images/hotel/max500/302379608.jpg?k=542b3a928e4d18cd1ba42a88974bd2cc105ebe6a805e2c9b2d14b64495d380c0&o=", "https://www.google.com.vn/images/branding/googlelogo/2x/googlelogo_color_92x30dp.png"];

                var jsonImages = Newtonsoft.Json.JsonConvert.SerializeObject(images);
                var paramerters = new DynamicParameters();
                paramerters.Add("@HotelId", RoomType.HotelId);
                paramerters.Add("@Name", RoomType.Name);
                paramerters.Add("@Description", RoomType.Description);
                paramerters.Add("@PricePerNight", RoomType.PricePerNight);
                paramerters.Add("@SingleBed", RoomType.SingleBed);
                paramerters.Add("@DoubleBed", RoomType.DoubleBed);
                paramerters.Add("@Capacity", RoomType.Capacity);
                paramerters.Add("@Sizes", RoomType.Sizes);
                paramerters.Add("@Thumbnail", RoomType.Thumbnail);
                paramerters.Add("@Images", jsonImages);
                paramerters.Add("@CreatedAt", DateTime.Now);
                paramerters.Add("@UpdatedAt", DateTime.Now);

                var result = _db.QueryFirstOrDefaultAsync<RoomTypes>("RoomTypes_Create", paramerters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public Task<int> DeleteRoomTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoomTypes>> GetRoomTypeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoomTypes> GetRoomTypeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RoomTypes> UpdateRoomTypeAsync(int id, UpdateRoomTypeDTO RoomType)
        {
            throw new NotImplementedException();
        }
    }
}