using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using MyToDo.Extensions;
using MyToDo.Shared;
using MyToDo.Shared.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient _httpRestRequest;
        private readonly string _serviceName;
        public BaseService(HttpRestClient httpRestRequest, string serviceName)
        {
            _httpRestRequest=httpRestRequest;
            _serviceName=serviceName;
        }

        public Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/{_serviceName}/Add",
                Method=Method.Post,
                Parameter=entity
            };
            return _httpRestRequest.ExecuteAsync<TEntity>(request);
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/{_serviceName}/Delete?id={id}",
                Method=Method.Get
            };
            return _httpRestRequest.ExecuteAsync(request);
        }

        public Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(PageQueryParameter pageQuery)
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/{_serviceName}/GetAll?pageIndex={pageQuery.PageIndex}&pageSize={pageQuery.PageSize}&keyword={pageQuery.Keyword}",
                Method=Method.Get
            };
            return _httpRestRequest.ExecuteAsync<PagedList<TEntity>>(request);
        }

        public Task<ApiResponse<TEntity>> GetFirstOrDefaultByIdAsync(int id)
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/{_serviceName}/GetById?id={id}",
                Method=Method.Get
            };
            return _httpRestRequest.ExecuteAsync<TEntity>(request);
        }

        public Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest()
            {
                Url=$"api/{_serviceName}/Update",
                Method=Method.Post,
                Parameter=entity
            };
            return _httpRestRequest.ExecuteAsync<TEntity>(request);

        }
    }
}
