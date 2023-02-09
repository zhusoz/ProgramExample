
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IToDoService : IBaseService<ToDoDto>
    {

        Task<ApiResponse<PagedList<ToDoDto>>> GetAllToDoAsync(ToDoQueryParameter parameter);

        Task<ApiResponse<SummaryDto>> SummaryAsync();
    }
}
