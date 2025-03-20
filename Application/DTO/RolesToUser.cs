using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Application.DTO
{
    public class RolesToUser
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}