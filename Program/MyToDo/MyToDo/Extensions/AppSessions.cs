using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ImTools.ImMap;

namespace MyToDo.Extensions
{

    public class AppSessionExtension
    {
        private const string LoginUserName = "LoginUserName";

        public static void SetLoginUserName(object value)
        {
            App.Current.Properties.Add(LoginUserName, value);
        }

        public static string? GetLoginUserName()
        {
            return (string?)App.Current.Properties[LoginUserName];
        }

        public static void RemoveLoginUserName()
        {
            App.Current.Properties.Remove(LoginUserName);
        }

    }
}
