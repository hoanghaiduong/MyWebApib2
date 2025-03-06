

using System.Data;
using Dapper;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Application.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _db;

        public UserService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<User> CreateUser(CreateUserDTO dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", dto.UserName);
            parameters.Add("@Email", dto.Email);

            var result = await _db.QueryFirstOrDefaultAsync<User>("CreateUser", parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }

        public async Task<User> CreateUserProfile(CreateUserProfileDTO dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", dto.UserId);
            parameters.Add("@FullName", dto.FullName);
            parameters.Add("@Address", dto.Address);
            parameters.Add("@Phone", dto.Phone);
            parameters.Add("@Avatar", dto.Avatar);
            parameters.Add("@DateOfBirth", dto.DateOfBirth);
            using var mutil = await _db.QueryMultipleAsync("CreateUserProfile", parameters, commandType: CommandType.StoredProcedure);
            //user && user profile
            // var result = await _db.QueryFirstOrDefaultAsync<User>("CreateUserProfile", parameters, commandType: CommandType.StoredProcedure);//inner join 
            var user = await mutil.ReadSingleOrDefaultAsync<User>();
            var profile=await mutil.ReadSingleOrDefaultAsync<UserProfile>();
            user.Profile=profile;
            return user;
        }

        public async Task<int> DeleteUser(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.ExecuteAsync("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<User> GetUserById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.QueryFirstOrDefaultAsync<User>("GetUserById", parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var result = await _db.QueryAsync<User>("GetAllUsers", commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<User> UpdateUser(int id, UpdateUserDTO dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Name", dto.UserName);
            parameters.Add("@Email", dto.Email);
            var result = await _db.QueryFirstOrDefaultAsync<User>("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }
}