using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Service;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;

namespace MyToDo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService=toDoService;
        }

        [HttpGet]
        public Task<ApiResponse> GetAll([FromQuery] ToDoQueryParameter pageQuery) => _toDoService.GetAllToDoAsync(pageQuery);

        [HttpGet]
        public Task<ApiResponse> GetById(int id) => _toDoService.GetByIdAsync(id);

        [HttpGet]
        public Task<ApiResponse> Summary() => _toDoService.SummaryAsync();

        [HttpPost]
        public Task<ApiResponse> Add(ToDoDto entity) => _toDoService.AddAsync(entity);

        [HttpPost]
        public Task<ApiResponse> Update(ToDoDto entity) => _toDoService.UpdateAsync(entity);

        [HttpGet]
        public Task<ApiResponse> Delete(int id) => _toDoService.DeleteAsync(id);


    }
}
