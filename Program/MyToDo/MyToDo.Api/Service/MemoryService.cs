using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;

namespace MyToDo.Api.Service
{
    public class MemoryService : IMemoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }


        public async Task<ApiResponse> AddAsync(MemoryDto entity)
        {
            var reposity = _unitOfWork.GetRepository<Memory>();
            var e = _mapper.Map<Memory>(entity);
            await reposity.InsertAsync(e);
            if (_unitOfWork.SaveChanges()>0)
                return new ApiResponse(_mapper.Map<MemoryDto>(e));
            return new ApiResponse(false, "插入失败");
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            var reposity = _unitOfWork.GetRepository<Memory>();
            reposity.Delete(id);
            if (_unitOfWork.SaveChanges()>0)
                return Task.FromResult(new ApiResponse(true));
            return Task.FromResult(new ApiResponse(false, "删除失败"));
        }

        public async Task<ApiResponse> GetAllAsync(PageQueryParameter pageQuery)
        {
            var reposity = _unitOfWork.GetRepository<Memory>();
            var data = await reposity.GetPagedListAsync(predicate: e => string.IsNullOrEmpty(pageQuery.Keyword) ? true : e.Title.Contains(pageQuery.Keyword),
                pageIndex: pageQuery.PageIndex,
                pageSize: pageQuery.PageSize,
                orderBy: source => source.OrderBy(e => e.Id));//To do...
            return new ApiResponse(data);
        }

        public async Task<ApiResponse> GetAllMemoryAsync(MemoryQueryParameter pageQuery)
        {
            var reposity = _unitOfWork.GetRepository<Memory>();
            var data = await reposity.GetPagedListAsync(predicate: e => string.IsNullOrEmpty(pageQuery.Keyword) ? true : e.Title.Contains(pageQuery.Keyword),
                pageIndex: pageQuery.PageIndex,
                pageSize: pageQuery.PageSize,
                orderBy: source => source.OrderBy(e => e.Id));
            return new ApiResponse(data);
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            var reposity = _unitOfWork.GetRepository<Memory>();
            var data = await reposity.GetFirstOrDefaultAsync(predicate: e => e.Id.Equals(id));
            return new ApiResponse(data);
        }

        public Task<ApiResponse> UpdateAsync(MemoryDto entity)
        {
            var reposity = _unitOfWork.GetRepository<Memory>();
            var e = _mapper.Map<Memory>(entity);
            reposity.Update(e);
            if (_unitOfWork.SaveChanges()>0)
                return Task.FromResult(new ApiResponse(_mapper.Map<MemoryDto>(e)));
            return Task.FromResult(new ApiResponse(false, "查找失败"));
        }
    }
}
