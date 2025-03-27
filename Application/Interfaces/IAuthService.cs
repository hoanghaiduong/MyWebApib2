using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Application.DTO;
using MyWebApi.Domain.Entities;
using MyWebApi.Infrastructure.Models;

namespace MyWebApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User> SignUp(AuthDTO dto);
        Task<User> SignIn(AuthDTO dto);
       
    }
}