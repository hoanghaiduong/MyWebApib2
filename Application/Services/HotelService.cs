using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyWebApi.Application.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IDbConnection _db;

        public HotelService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            //
            // @Name NVARCHAR(MAX),
            // @Quantity INT,
            // @Descriptions NVARCHAR(MAX) = NULL,
            // @Notes NVARCHAR(MAX) = NULL,
            // @Address NVARCHAR(MAX),
            // @Location NVARCHAR(MAX) = NULL,
            // @Phone NVARCHAR(MAX),
            // @Email NVARCHAR(MAX),
            // @Thumbnail NVARCHAR(MAX),
            // @Images NVARCHAR(MAX),
            // @Stars INT = NULL,
            // @Floor INT = NULL,
            // @CheckinTime NVARCHAR(MAX) = NULL,
            // @CheckoutTime NVARCHAR(MAX) = NULL,
            // @CreatedAt DATETIME2(7) = NULL,
            // @UpdatedAt DATETIME2(7) = NULL
            try
            {
                var paramerters = new DynamicParameters();
                paramerters.Add("@Name", hotel.Name);
                paramerters.Add("@Quantity", hotel.Quantity);
                paramerters.Add("@Descriptions", hotel.Descriptions);
                paramerters.Add("@Notes", hotel.Notes);
                paramerters.Add("@Address", hotel.Address);
                paramerters.Add("@Location", hotel.Location);
                paramerters.Add("@Phone", hotel.Phone);
                paramerters.Add("@Email", hotel.Email);
                paramerters.Add("@Thumbnail", hotel.Thumbnail);
                paramerters.Add("@Images", hotel.Images);
                paramerters.Add("@Stars", hotel.Stars);
                paramerters.Add("@Floor", hotel.Floor);
                paramerters.Add("@CheckinTime", hotel.CheckInTime);
                paramerters.Add("@CheckoutTime", hotel.CheckOutTime);
                paramerters.Add("@CreatedAt", DateTime.Now);
                paramerters.Add("@UpdatedAt", DateTime.Now);

                var result = await _db.QueryFirstOrDefaultAsync<Hotel>("CreateHotel", paramerters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public Task<int> DeleteHotelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            var paramerters = new DynamicParameters();
            paramerters.Add("@Id", id);
            using var multi = await _db.QueryMultipleAsync("Hotels_GetByID", paramerters, commandType: CommandType.StoredProcedure);
            var hotel = await multi.ReadSingleOrDefaultAsync<Hotel>();
            var roomTypes = (await multi.ReadAsync<RoomTypes>()).ToList();
            hotel.RoomTypes = roomTypes;
            return hotel;
        }

        public Task<List<Hotel>> GetHotelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> UpdateHotelAsync(int id, Hotel hotel)
        {
            throw new NotImplementedException();
        }
    }
}