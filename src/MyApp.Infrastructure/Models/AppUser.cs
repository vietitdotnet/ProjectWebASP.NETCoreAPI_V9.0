using Microsoft.AspNetCore.Identity;
using MyApp.Domain.Abstractions;
using MyApp.Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace MyApp.Infrastructure.Models
{
    public class AppUser : IdentityUser, IAppUserReference
    {
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        public AuthProvider Provider { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        //Dùng để vô hiệu hóa toàn bộ AccessToken + RefreshToken cũ
        public int TokenVersion { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public string Avartar { get; set; }

    }
}
