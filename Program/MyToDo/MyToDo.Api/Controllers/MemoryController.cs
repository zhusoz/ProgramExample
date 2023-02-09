using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;

namespace MyToDo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private readonly IMemoryService _memoryService;

        public MemoryController(IMemoryService memoryService)
        {
            _memoryService=memoryService;
        }

        [HttpGet]
        public Task<ApiResponse> GetAll([FromQuery]MemoryQueryParameter pageQuery) => _memoryService.GetAllMemoryAsync(pageQuery);

        [HttpGet]
        public Task<ApiResponse> GetById(int id) => _memoryService.GetByIdAsync(id);

        [HttpPost]
        public Task<ApiResponse> Add(MemoryDto entity) => _memoryService.AddAsync(entity);

        [HttpPost]
        public Task<ApiResponse> Update(MemoryDto entity) => _memoryService.UpdateAsync(entity);

        [HttpGet]
        public Task<ApiResponse> Delete(int id) => _memoryService.DeleteAsync(id);
    }
}
