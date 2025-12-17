using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Models.Requests.Registers
{
    public class RegisterRequest
    {

        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
