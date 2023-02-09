using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Common.Aop
{
    public class LogAop : IInterceptor
    {
        //private readonly IHttpContextAccessor _accessor;

        //public LogAop(IHttpContextAccessor accessor)
        //{
        //    _accessor = accessor;
        //}

        public void Intercept(IInvocation invocation)
        {
            ////string UserName = _accessor.HttpContext?.User?.Identity?.Name;
            ////var dataIntercept = "" +
            //// $"【当前操作用户】：{ UserName} \r\n" +
            //// $"【当前执行方法】：{ invocation.Method.Name} \r\n" +
            //// $"【携带的参数有】： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";
            ////Console.WriteLine(dataIntercept);

            //Console.WriteLine("实现方法之前");
            ////在被拦截的方法执行完毕后 继续执行当前方法，注意是被拦截的是异步的
            //invocation.Proceed();
            //Console.WriteLine("实现方法之后");

            BeforeProceed(invocation);
           
            invocation.Proceed();
            if (IsAsyncMethod(invocation.MethodInvocationTarget))
            {
                // 关键实现语句
                invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue, invocation);
            }
            else
            {
                AfterProceedSync(invocation);
            }
        }


        private bool CheckMethodReturnTypeIsTaskType(MethodInfo method)
        {
            var methodReturnType = method.ReturnType;
            if (methodReturnType.IsGenericType)
            {
                if (methodReturnType.GetGenericTypeDefinition() == typeof(Task<>) ||
                    methodReturnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
                    return true;
            }
            else
            {
                if (methodReturnType == typeof(Task) ||
                    methodReturnType == typeof(ValueTask))
                    return true;
            }
            return false;
        }

        private bool IsAsyncMethod(MethodInfo method)
        {
            bool isDefAsync = Attribute.IsDefined(method, typeof(AsyncStateMachineAttribute), false);
            bool isTaskType = CheckMethodReturnTypeIsTaskType(method);
            bool isAsync = isDefAsync && isTaskType;

            return isAsync;
        }

        protected object ProceedAsyncResult { get; set; }


        private async Task InterceptAsync(Task task, IInvocation invocation)
        {
            await task.ConfigureAwait(false);
            await AfterProceedAsync(invocation, false);
        }

        private async Task<TResult> InterceptAsync<TResult>(Task<TResult> task, IInvocation invocation)
        {
            ProceedAsyncResult = await task.ConfigureAwait(false);
            await AfterProceedAsync(invocation, true);
            return (TResult)ProceedAsyncResult;
        }

        private async ValueTask InterceptAsync(ValueTask task, IInvocation invocation)
        {
            await task.ConfigureAwait(false);
            await AfterProceedAsync(invocation, false);
        }

        private async ValueTask<TResult> InterceptAsync<TResult>(ValueTask<TResult> task, IInvocation invocation)
        {
            ProceedAsyncResult = await task.ConfigureAwait(false);
            await AfterProceedAsync(invocation, true);
            return (TResult)ProceedAsyncResult;
        }

        protected virtual void BeforeProceed(IInvocation invocation) { }

        protected virtual void AfterProceedSync(IInvocation invocation) { }

        protected virtual Task AfterProceedAsync(IInvocation invocation, bool hasAsynResult)
        {
            return Task.CompletedTask;
        }
    }
}
