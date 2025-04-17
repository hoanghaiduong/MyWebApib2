using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IRedisCacheService _redis;
        private readonly IDbConnection _db;

        public TestController(IRedisCacheService redis, IDbConnection db)
        {
            _redis = redis;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> TestMethod()
        {
            var dataCaching = _redis.GetData<List<Role>>("roles");
            if (dataCaching is not null && dataCaching.Count != 0)
            {
                return Ok(new { cache = dataCaching });
            }
            var roles = await _db.QueryAsync<Role>("SELECT * FROM Roles", commandType: CommandType.Text);
            _redis.SetData("roles", roles.ToList());
            return Ok(new
            {
                database = roles
            });
        }
    }

}