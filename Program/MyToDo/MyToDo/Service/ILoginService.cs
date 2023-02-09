using MyToDo.Shared;
using MyToDo.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> LoginAsync(UserDto dto);

        Task<ApiResponse<UserDto>> RegisterAsync(UserDto dto);
    }
}
