using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using MyWebApi.Application.DTO;
using MyWebApi.Application.Interfaces;
using MyWebApi.Domain.Entities;
using MyWebApi.Infrastructure.Models;

namespace MyWebApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbConnection _db;
        public AuthService(IDbConnection db)
        {
            _db = db;

        }

        

        public async Task<User> SignIn(AuthDTO dto)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", dto.Email);
                parameters.Add("@Password", dto.Password);
                using var multi = await _db.QueryMultipleAsync("Users_CheckLogin", parameters, commandType: CommandType.StoredProcedure);

                var user = await multi.ReadSingleAsync<User>();
                var roles = (await multi.ReadAsync<Role>()).ToList();
                user.Roles = roles;
             
                return user;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> SignUp(AuthDTO dto)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", dto.Email);
                parameters.Add("@Password", dto.Password); // Cần mã hóa trước khi lưu vào DB
                var result = await _db.QueryFirstOrDefaultAsync<User>("Users_Create", parameters, commandType: CommandType.StoredProcedure);
                return result!;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}