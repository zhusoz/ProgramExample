using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProblemDisposal.Common.Basic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataSharing.Filter
{
    public class ApiValidationFilter : IAsyncActionFilter
    {
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.GetValidationSummary();
                var result = new AjaxResult<bool>()
                {
                    Success=false,
                    Message=errors
                };

                context.Result=new JsonResult(result);
            }
            else
            {
                await next();
            }
        }
    }
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取验证消息提示并格式化提示
        /// </summary>
        public static string GetValidationSummary(this ModelStateDictionary modelState, string separator = "\r\n")
        {
            if (modelState.IsValid) return null;

            var error = new StringBuilder();

            foreach (var item in modelState)
            {
                var state = item.Value;
                var message = state.Errors.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.ErrorMessage))?.ErrorMessage;
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = state.Errors.FirstOrDefault(o => o.Exception != null)?.Exception.Message;
                }
                if (string.IsNullOrWhiteSpace(message)) continue;

                if (error.Length > 0)
                {
                    error.Append(separator);
                }

                error.Append(message);
            }

            return error.ToString();
        }
    }
}
