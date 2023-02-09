using Microsoft.AspNetCore.Builder;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Common.Quartz
{
    public class Quartz
    {
        public static async Task Start()
        {
            try
            {
                //从工厂获取一个调度器实例化
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                //创建一个作业
                IJobDetail job = JobBuilder.Create<DeleteLogJob>().WithIdentity("定时删除日志").Build();
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("日志删除")
                    .StartAt(DateTime.Now + TimeSpan.FromHours(24))
                    .WithSimpleSchedule(x =>
                    {
                        x.WithIntervalInHours(24).RepeatForever();
                    }).Build();

              
                await scheduler.ScheduleJob(job, trigger);

               await scheduler.Start();
            }
            catch (Exception ex)
            {
                LogHelper.QuartzTaskWriteToLocal($"---------------------定时任务异常-----------------------\r\n{ex.Message}\r\n{ex.StackTrace}");
            }


        }
    }
}
