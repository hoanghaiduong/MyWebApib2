using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Infrastructure.Models
{
    public class AppSettings
    {
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}