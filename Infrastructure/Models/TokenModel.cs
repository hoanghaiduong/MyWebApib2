using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Infrastructure.Models
{
    public record TokenModel(string AccessToken,string RefreshToken);
}