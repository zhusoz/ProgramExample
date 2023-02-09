using Microsoft.Extensions.DependencyInjection;
using ProgramsNetCore.IService;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace ProgramsNetCore.Common
{
    public static class ServiceHelper
    {
        static IServiceScope serviceScope = ServiceLocator.ServiceScope;
        public static T GetService<T>() where T : class
        {
           return serviceScope.ServiceProvider.GetService(typeof(T)) as T;
        }
    }
}
