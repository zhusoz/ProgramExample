using MyToDo.Shared;
using MyToDo.Shared.Utils;

namespace MyToDo.Api.Service
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponse> AddAsync(T entity);

        Task<ApiResponse> DeleteAsync(int id);
        
        Task<ApiResponse> GetByIdAsync(int id);
        
        Task<ApiResponse> GetAllAsync(PageQueryParameter pageQuery);
        
        Task<ApiResponse> UpdateAsync(T entity);
    }
}
