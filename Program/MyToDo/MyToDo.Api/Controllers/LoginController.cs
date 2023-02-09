using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;
using MyToDo.Shared;
using MyToDo.Shared.Dto;

namespace MyToDo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService=loginService;
        }

        [HttpPost]
        public Task<ApiResponse> LoginAsync(UserDto user) => _loginService.LoginAsync(user);

        [HttpPost]
        public Task<ApiResponse> RegisterAsync(UserDto user) => _loginService.RegistAsync(user);


    }
}
