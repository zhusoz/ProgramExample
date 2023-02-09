using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProgramsNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Common.Extensions
{
    public static class LoggerExtensions
    {
        public static IApplicationBuilder UseDbLoggerRecord(this IApplicationBuilder app)
        {
            var iLogger = app.ApplicationServices.GetRequiredService<ILogger>();
            LoggerModel.Configure(iLogger);
            return app;
        }
    }
}
