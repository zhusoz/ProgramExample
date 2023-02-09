using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;
using System.Collections.ObjectModel;

namespace MyToDo.Api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }


        public async Task<ApiResponse> AddAsync(ToDoDto entity)
        {
            var reposity = _unitOfWork.GetRepository<ToDo>();
            var e = _mapper.Map<ToDo>(entity);
            await reposity.InsertAsync(e);
            if (_unitOfWork.SaveChanges()>0)
                return new ApiResponse(_mapper.Map<ToDoDto>(e));
            return new ApiResponse(false, "插入失败");
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            var reposity = _unitOfWork.GetRepository<ToDo>();
            reposity.Delete(id);
            if (_unitOfWork.SaveChanges()>0)
                return Task.FromResult(new ApiResponse(true));
            return Task.FromResult(new ApiResponse(false, "删除失败"));
        }

        public async Task<ApiResponse> GetAllAsync(PageQueryParameter pageQuery)
        {
            var reposity = _unitOfWork.GetRepository<ToDo>();
            var data = await reposity.GetPagedListAsync(predicate: e => string.IsNullOrEmpty(pageQuery.Keyword) ? true : e.Title.Contains(pageQuery.Keyword),
                pageIndex: pageQuery.PageIndex,
                pageSize: pageQuery.PageSize,
                orderBy: source => source.OrderBy(e => e.Id));//To do...
            //var data = await reposity.GetAllAsync();
            return new ApiResponse(data);
        }

        public async Task<ApiResponse> GetAllToDoAsync(ToDoQueryParameter pageQuery)
        {
            var reposity = _unitOfWork.GetRepository<ToDo>();
            var data = await reposity.GetPagedListAsync(predicate: e => (string.IsNullOrEmpty(pageQuery.Keyword) ? true : e.Title.Contains(pageQuery.Keyword)) && (pageQuery.Status==null) ? true : e.Status.Equals(pageQuery.Status),
                pageIndex: pageQuery.PageIndex,
                pageSize: pageQuery.PageSize,
                orderBy: source => source.OrderBy(e => e.Id));
            return new ApiResponse(data);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var reposity = _unitOfWork.GetRepository<ToDo>();
            var data = await reposity.GetFirstOrDefaultAsync(predicate: e => e.Id.Equals(id));
            return new ApiResponse(data);
        }

        public async Task<ApiResponse> SummaryAsync()
        {
            var todoReposity = _unitOfWork.GetRepository<ToDo>();
            var memoryReposity = _unitOfWork.GetRepository<Memory>();
            SummaryDto dto = new SummaryDto();
            var allTodo = await todoReposity.GetAllAsync(orderBy: source => source.OrderByDescending(t => t.CreateTime));
            var allMemory = await memoryReposity.GetAllAsync(orderBy: source => source.OrderByDescending(t => t.CreateTime));
            dto.TotalCount= allTodo.Count;
            dto.MemoryCount=allMemory.Count;
            dto.TodoList=new ObservableCollection<ToDoDto>(_mapper.Map<List<ToDoDto>>(allTodo.Where(e => e.Status==0)));
            dto.MemoryList=new ObservableCollection<MemoryDto>(_mapper.Map<List<MemoryDto>>(allMemory));
            dto.HasFinishedCount=allTodo.Where(e => e.Status==1).Count();
            dto.CompletedRate=((double)dto.HasFinishedCount/dto.TotalCount).ToString("0%");
            return new ApiResponse(dto);
        }

        public Task<ApiResponse> UpdateAsync(ToDoDto entity)
        {
            var reposity = _unitOfWork.GetRepository<ToDo>();
            var e = _mapper.Map<ToDo>(entity);
            reposity.Update(_mapper.Map<ToDo>(e));
            if (_unitOfWork.SaveChanges()>0)
                return Task.FromResult(new ApiResponse(_mapper.Map<ToDoDto>(e)));
            return Task.FromResult(new ApiResponse(false, "查找失败"));
        }
    }
}
