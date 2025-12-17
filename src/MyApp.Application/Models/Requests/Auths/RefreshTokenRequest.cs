using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Models.Requests.Auths
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }   
        public string RefreshToken { get; set; }
    }
}
