using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Models.Enums
{
    public enum AuthProvider
    {
        Local = 0,    // Tài khoản nội bộ, dùng mật khẩu
        Google = 1,   // Google OAuth
        Facebook = 2, // Facebook OAuth
        Apple = 3     // Apple SignIn
    }
}
