using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApp.Application.Core.Services;
using MyApp.Infrastructure.Services;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Extentions
{
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder UseCustomLogging(this WebApplicationBuilder builder)
        {     
            builder.Host.UseNLog();           
            return builder;
        }

       
    }
}
