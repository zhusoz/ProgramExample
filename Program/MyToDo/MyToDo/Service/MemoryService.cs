using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using MyToDo.Extensions;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class MemoryService : BaseService<MemoryDto>, IMemoryService
    {
        private readonly HttpRestClient _httpRestRequest;

        public MemoryService(HttpRestClient httpRestRequest, string serviceName = "Memory") : base(httpRestRequest, serviceName)
        {
            _httpRestRequest=httpRestRequest;
        }
        
        public Task<ApiResponse<PagedList<MemoryDto>>> GetAllMemoryAsync(MemoryQueryParameter parameter)
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/Memory/GetAll?pageIndex={parameter.PageIndex}&pageSize={parameter.PageSize}&keyword={parameter.Keyword}",
                Method=Method.Get
            };
            return _httpRestRequest.ExecuteAsync<PagedList<MemoryDto>>(request);
        }
    }
}
