using Microsoft.Extensions.DependencyInjection;
using ProgramsNetCore.IService;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Common.Quartz
{
    public class DeleteLogJob:IJob
    {
        private static IPlatformLogService PlatformLogService => ServiceHelper.GetService<IPlatformLogService>();
        private static IDbLogService DbLogService => ServiceHelper.GetService<IDbLogService>();
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(async () =>
            {
                await PlatformLogService.Delete(m => m.CreateTime < DateTime.Now.AddDays(-182));
                await DbLogService.Delete(m => m.CreateTime < DateTime.Now.AddDays(-182));
                LogHelper.QuartzTaskWriteToLocal("\r\n---------------------删除日志-----------------------");
            });
        }
    }
}
