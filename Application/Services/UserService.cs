

using System.Data;
using Dapper;
using MyWebApi.Application.DTO;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Application.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _db;
        private readonly IFileUploadService _fileUploadService;
        public UserService(IDbConnection db, IFileUploadService fileUploadService)
        {
            _db = db;
            _fileUploadService = fileUploadService;
        }

        public async Task<User> AssignRoleToUser(UserRolesDTO dto)
        {
            try
            {
                // @UserId INT,
                // @RoleId INT
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", dto.UserId);
                parameters.Add("@RoleId", dto.RoleId);
                using var multi = await _db.QueryMultipleAsync("UserRoles_Create", parameters, commandType: CommandType.StoredProcedure);
                var user = await multi.ReadFirstOrDefaultAsync<User>() ?? throw new Exception("User not found");
                var roles = (await multi.ReadAsync<Role>()).ToList();
                user.Roles = roles;
                return user;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<User> AssignRolesToUser(RolesToUser dto)
        {
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("RoleId", Type.GetType("System.Int32"));
                if (dto.RoleIds.Count != 0)
                {
                    foreach (var item in dto.RoleIds)
                    {
                        dt.Rows.Add(item);
                    }
                }



                var parameters = new DynamicParameters();
                parameters.Add("@UserId", dto.UserId);
                parameters.Add("@Roles", dt.AsTableValuedParameter("dbo.UserRoleType"));
                using var multi = await _db.QueryMultipleAsync("UserRoles_CreateMultiple", parameters, commandType: CommandType.StoredProcedure);
                var user = await multi.ReadFirstOrDefaultAsync<User>() ?? throw new Exception("User not found");
                var roles = (await multi.ReadAsync<Role>()).ToList();
                user.Roles = roles;
                return user;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<User> CreateUser(CreateUserDTO dto)
        {

            var avatar = string.Empty;
            try
            {
                if (dto.Avatar != null)
                {
                    avatar = await _fileUploadService.UploadSingleFile(["uploads", "users"], dto.Avatar);
                }
                var parameters = new DynamicParameters();
                parameters.Add("@Email", dto.Email);
                parameters.Add("@Password", dto.Password); // Cần mã hóa trước khi lưu vào DB
                parameters.Add("@Address", dto.Address);
                parameters.Add("@DateOfBirth", dto.DateOfBirth);
                parameters.Add("@PhoneNumber", dto.PhoneNumber);
                parameters.Add("@FullName", dto.FullName);
                parameters.Add("@EmailVerified", dto.EmailVerified ?? false);
                parameters.Add("@Avatar", avatar);
                parameters.Add("@RefreshToken", dto.RefreshToken);
                parameters.Add("@IsDisabled", dto.IsDisabled ?? false);
                parameters.Add("@LastLogin", dto.LastLogin);
                parameters.Add("@HotelId", dto.HotelId);
                parameters.Add("@CreatedAt", DateTime.UtcNow); // Lấy thời gian hiện tại nếu không có giá trị
                parameters.Add("@UpdatedAt", DateTime.UtcNow);

                var result = await _db.QueryFirstOrDefaultAsync<User>("Users_Create", parameters, commandType: CommandType.StoredProcedure);
                return result!;
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(avatar))
                {
                    _fileUploadService.DeleteSingleFile(avatar);
                }
                throw;
            }

        }

      
        public async Task<int> DeleteUser(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.ExecuteAsync("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<User> GetUserById(int id, int depth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Depth", depth);
            using var multi = await _db.QueryMultipleAsync("Users_GetByID", parameters, commandType: CommandType.StoredProcedure);
            var user = await multi.ReadFirstOrDefaultAsync<User>() ?? throw new Exception("User not found");
            if (depth >= 1)
            {
                var roles = (await multi.ReadAsync<Role>()).ToList();
                user.Roles = roles;
            }

            return user;
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
            // parameters.Add("@Name", dto.UserName);
            parameters.Add("@Email", dto.Email);
            var result = await _db.QueryFirstOrDefaultAsync<User>("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            return result!;
        }
    }
}