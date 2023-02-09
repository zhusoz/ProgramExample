using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared;
using MyToDo.Shared.Dto;

namespace MyToDo.Api.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task<ApiResponse> LoginAsync(UserDto user)
        {
            var entity = await _unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(predicate: e => e.Account.Equals(user.Account)&& e.Password.Equals(user.Password.GetMD5()));
            if (entity == null)
                return new ApiResponse(false, $"用户名或密码错误.");
            return new ApiResponse(_mapper.Map<UserDto>(entity));
        }

        public async Task<ApiResponse> RegistAsync(UserDto user)
        {
            user.Password=user.Password.GetMD5();
            var entity = _mapper.Map<User>(user);
            var repository = _unitOfWork.GetRepository<User>();
            var entityModel = await repository.GetFirstOrDefaultAsync(predicate: e => e.Account.Equals(user.Account));
            if (entityModel!=null)
                return new ApiResponse(false, $"账号{user.Account}已存在.");

            await repository.InsertAsync(entity);
            if (await _unitOfWork.SaveChangesAsync()>0)
                return new ApiResponse(_mapper.Map<UserDto>(entity));
            return new ApiResponse(false, "Error");
        }
    }
}
