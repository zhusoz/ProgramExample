using MyToDo.Shared;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyToDo.Extensions
{
    public class HttpRestClient
    {
        private readonly RestClient _client;
        private readonly string _baseUrl;

        public HttpRestClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            RestRequest request = new RestRequest(new Uri(_baseUrl+baseRequest.Url), baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter!=null)
                request.AddJsonBody(baseRequest.Parameter);
            //_client.AddDefaultParameter("param", JsonSerializer.Serialize(baseRequest.Parameter));

            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ApiResponse(false);
            }
            return JsonSerializer.Deserialize<ApiResponse>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive=true });
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest) where T : class
        {
            RestRequest request = new RestRequest(new Uri(_baseUrl+baseRequest.Url), baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter!=null)
                request.AddJsonBody(baseRequest.Parameter);
            //_client.AddDefaultParameter("param", JsonSerializer.Serialize(baseRequest.Parameter));

            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ApiResponse<T>();
            }
            return JsonSerializer.Deserialize<ApiResponse<T>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive=true });

        }

    }

}
