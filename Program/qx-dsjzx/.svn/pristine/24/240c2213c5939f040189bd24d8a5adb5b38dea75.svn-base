using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ProgramsNetCore.Common
{
    public static class ToolExpand
    {
        public static int ToInt32(this object obj)
        {
            try
            {
                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
                else return -1;

            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取当前httpcontext中的uid
        /// </summary>
        /// <param name="obj">httpcontext</param>
        /// <returns></returns>
        public static string GetContextUser(this object obj)
        {
            try
            {

               
                if (obj.GetType().ToString().Equals("Microsoft.AspNetCore.Http.DefaultHttpContext"))
                {
                    return (obj as HttpContext).User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                }
                else
                    return "请传入正确类型";
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}
    
