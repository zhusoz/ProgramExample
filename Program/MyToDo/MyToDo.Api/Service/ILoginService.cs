using MyToDo.Shared;
using MyToDo.Shared.Dto;

namespace MyToDo.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(UserDto user);

        Task<ApiResponse> RegistAsync(UserDto user);
    }
}
