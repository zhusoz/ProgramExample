using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;

namespace MyToDo.Api.Service
{
    public interface IMemoryService : IBaseService<MemoryDto>
    {
        Task<ApiResponse> GetAllMemoryAsync(MemoryQueryParameter pageQuery);
    }
}
