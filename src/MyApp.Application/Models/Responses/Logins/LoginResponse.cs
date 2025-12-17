using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Models.Responses.Logins
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LoginResponseData Data { get; set; }
    }
}
