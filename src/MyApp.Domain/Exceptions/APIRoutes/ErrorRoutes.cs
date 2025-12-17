using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Exceptions.APIRoutes
{
    public static class ErrorRoutes
    {
        public const string NotFound = "/api/error/not-found";
        public const string BadRequest = "/api/error/bad-request";
        public const string Database = "/api/error/database-error";
        public const string Unauthorized = "/api/error/unauthorized";
        public const string Unknown = "/api/error/unknown";
    }
}
