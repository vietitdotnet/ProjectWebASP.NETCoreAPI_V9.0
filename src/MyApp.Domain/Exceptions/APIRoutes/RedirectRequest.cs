using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Exceptions.APIRoutes
{
    public static class RedirectRequest
    {
        public const string Index = "/api/error/redirect"; // mặc định chung
       
        public const string ConfirmedEmail = "/api/user/confirmed-email";
       
        public const string PaymentRequired = "/api/payment/start";
        
        public const string LoginRequired = "/api/auth/login";

        public const string LockoutRequired = "/api/auth/lockout";
    }
}
