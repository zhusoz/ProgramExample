using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Common.Extensions
{
    public static class QuartzExtension
    {
        public static IApplicationBuilder UseQuartz(this IApplicationBuilder app)
        {
            Quartz.Quartz.Start().GetAwaiter().GetResult();
            return app;
        }
    }
}
