using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;

namespace MyToDo.Api.Service
{
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllToDoAsync(ToDoQueryParameter pageQuery);

        Task<ApiResponse> SummaryAsync();
    }
}
