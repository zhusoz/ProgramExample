using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProgramsNetCore.Common
{
    public static class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
        public static IApplicationBuilder ApplicationBuilder { get; set; }
        public static IServiceScope ServiceScope => ApplicationBuilder.ApplicationServices.CreateScope();
    }
}
