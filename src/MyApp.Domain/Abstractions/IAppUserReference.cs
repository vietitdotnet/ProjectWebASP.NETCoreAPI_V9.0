using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Abstractions
{
    public interface IAppUserReference
    {
        string Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        public int TokenVersion { get; set; }
        string RefreshToken { get; }
        DateTime? RefreshTokenExpiryTime { get; }
    }
}
