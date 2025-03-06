using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.DTO.Creates;
using MyWebApi.Application.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfileToUser([FromBody] CreateUserProfileDTO dto)
        {
          var result = await _userService.CreateUserProfile(dto);
          return result==null? BadRequest(): Ok(new {result});
        }
    }
}