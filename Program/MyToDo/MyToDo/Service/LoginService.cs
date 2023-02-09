using MyToDo.Extensions;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Login";

        public LoginService(HttpRestClient client)
        {
            this.client=client;
        }

        public Task<ApiResponse<UserDto>> LoginAsync(UserDto dto)
        {
            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Url=$"api/{serviceName}/Login";
            baseRequest.Method=RestSharp.Method.Post;
            baseRequest.Parameter = dto;
            return client.ExecuteAsync<UserDto>(baseRequest);
        }

        public Task<ApiResponse<UserDto>> RegisterAsync(UserDto dto)
        {
            BaseRequest baseRequest = new BaseRequest();
            baseRequest.Url=$"api/{serviceName}/Register";
            baseRequest.Method=RestSharp.Method.Post;
            baseRequest.Parameter = dto;
            return client.ExecuteAsync<UserDto>(baseRequest);
        }
    }
}
