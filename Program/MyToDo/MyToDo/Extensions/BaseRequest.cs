using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Extensions
{
    public class BaseRequest
    {
        // 请求方式
        public Method Method { get; set; } = Method.Get;

        // 接口地址
        public string Url { get; set; }

        // 参数
        public object Parameter { get; set; }

        // 
        public string ContentType { get; set; } = "application/json";

    }
}
