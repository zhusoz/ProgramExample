//using Arch.EntityFrameworkCore.UnitOfWork;

//namespace MyToDo.Api.Service
//{
//    public class BaseService<T> : IBaseService<T> where T : class
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public BaseService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork=unitOfWork;
//        }

//        public async Task<ApiResponse> AddAsync(T entity)
//        {
//            var reposity = _unitOfWork.GetRepository<T>();
//            await reposity.InsertAsync(entity);
//            if (_unitOfWork.SaveChanges()>0)
//                return new ApiResponse(true);
//            return new ApiResponse(false, "插入失败");
//        }

//        public Task<ApiResponse> DeleteAsync(int id)
//        {
//            var reposity = _unitOfWork.GetRepository<T>();
//            reposity.Delete(id);
//            if (_unitOfWork.SaveChanges()>0)
//                return Task.FromResult(new ApiResponse(true));
//            return Task.FromResult(new ApiResponse(false, "删除失败"));
//        }

//        public async Task<ApiResponse> GetAllAsync()
//        {
//            var reposity = _unitOfWork.GetRepository<T>();
//            var data = await reposity.GetAllAsync();
//            if (_unitOfWork.SaveChanges()>0)
//                return new ApiResponse(data);
//            return new ApiResponse(false, "查找失败");
//        }

//        public async Task<ApiResponse> GetByIdAsync(int id)
//        {
//            var reposity = _unitOfWork.GetRepository<T>();
//            var data = await reposity.GetFirstOrDefaultAsync(predicate:(T e)=>e.Id==id);
//            if (_unitOfWork.SaveChanges()>0)
//                return new ApiResponse(data);
//            return new ApiResponse(false, "查找失败");
//        }

//        public Task<ApiResponse> UpdateAsync(T entity)
//        {
//            var reposity = _unitOfWork.GetRepository<T>();
//            var data = await reposity.GetFirstOrDefaultAsync(predicate: (T e) => e.Id==id);
//            if (_unitOfWork.SaveChanges()>0)
//                return new ApiResponse(data);
//            return new ApiResponse(false, "查找失败");
//        }
//    }
//}
