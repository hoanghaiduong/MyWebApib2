

using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserProfile(CreateUserProfileDTO dto);
        Task<User> CreateUser(CreateUserDTO dto);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> UpdateUser(int id,UpdateUserDTO dto);
        Task<int> DeleteUser(int id);
    }
}