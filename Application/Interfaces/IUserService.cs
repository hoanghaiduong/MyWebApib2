

using MyWebApi.Application.DTO;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.DTO.Updates;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> AssignRoleToUser(UserRolesDTO dto);
         Task<User> AssignRolesToUser(RolesToUser dto);
        Task<User> CreateUserProfile(CreateUserProfileDTO dto);
        Task<User> CreateUser(CreateUserDTO dto);
        Task<User> GetUserById(int id,int depth);
        Task<IEnumerable<User>> GetUsers();
        Task<User> UpdateUser(int id,UpdateUserDTO dto);
        Task<int> DeleteUser(int id);
    }
}