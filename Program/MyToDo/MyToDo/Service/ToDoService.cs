
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using MyToDo.Extensions;
using MyToDo.Shared;
using MyToDo.Shared.Dto;
using MyToDo.Shared.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient _httpRestRequest;

        public ToDoService(HttpRestClient httpRestRequest) : base(httpRestRequest, "ToDo")
        {
            _httpRestRequest=httpRestRequest;
        }

        public Task<ApiResponse<PagedList<ToDoDto>>> GetAllToDoAsync(ToDoQueryParameter parameter)
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/ToDo/GetAll?pageIndex={parameter.PageIndex}&pageSize={parameter.PageSize}&keyword={parameter.Keyword}&status={parameter.Status}",
                Method=Method.Get
            };
            return _httpRestRequest.ExecuteAsync<PagedList<ToDoDto>>(request);
        }

        public Task<ApiResponse<SummaryDto>> SummaryAsync()
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/ToDo/Summary",
                Method=Method.Get
            };
            return _httpRestRequest.ExecuteAsync<SummaryDto>(request);
        }
    }
}
