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
    public interface IMemoryService:IBaseService<MemoryDto>
    {
        Task<ApiResponse<PagedList<MemoryDto>>> GetAllMemoryAsync(MemoryQueryParameter pageQuery);
    }
}
