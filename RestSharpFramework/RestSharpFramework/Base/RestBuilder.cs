using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpFramework.Base
{
    public interface IRestBuilder
    {
        IRestBuilder WithBody(object body);
        IRestBuilder WithHeader(string name, string value);
        IRestBuilder WithQueryParameter(string name, string value);
        IRestBuilder WithRequest(string request);
        IRestBuilder WithRequest(string request, Method method);
        IRestBuilder WithUrlSegment(string name, string value);
        IRestBuilder WithFile(string name, string filePath, string? contentType = null);
        Task<T?> WithPatch<T>();
        Task<T?> WithPost<T>();
        Task<RestResponse> WithPost();
        Task<T?> WithPut<T>();
        Task<T?> WithDelete<T>();
        Task<T?> WithGet<T>();
        Task<RestResponse> WithAsync();
    }

    public class RestBuilder : IRestBuilder
    {
        private readonly IRestLibrary restLibrary;

        public RestBuilder(IRestLibrary restLibrary)
        {
            this.restLibrary = restLibrary;
        }

        private RestRequest RestRequest { get; set; } = null!;

        public IRestBuilder WithRequest(string request)
        {
            RestRequest = new RestRequest(request);
            return this;
        }

        public IRestBuilder WithHeader(string name, string value)
        {
            RestRequest.AddHeader(name, value);
            return this;
        }

        public IRestBuilder WithQueryParameter(string name, string value)
        {
            RestRequest.AddQueryParameter(name, value);
            return this;
        }

        public IRestBuilder WithUrlSegment(string name, string value)
        {
            RestRequest.AddUrlSegment(name, value);
            return this;
        }

        public IRestBuilder WithBody(object body)
        {
            RestRequest.AddJsonBody(body);
            return this;
        }

        public IRestBuilder WithFile(string name, string filePath, string? contentType = null)
        {
            RestRequest.AddFile(name, filePath, contentType);
            return this;
        }

        public IRestBuilder WithRequest(string request, Method method)
        {
            RestRequest = new RestRequest(request, method);
            return this;
        }

        public async Task<T?> WithGet<T>()
        {
            return await restLibrary.RestClient.GetAsync<T>(RestRequest);
        }

        public async Task<T?> WithPost<T>()
        {
            return await restLibrary.RestClient.PostAsync<T>(RestRequest);
        }

        public async Task<RestResponse> WithPost()
        {
            return await restLibrary.RestClient.PostAsync(RestRequest);
        }

        public async Task<T?> WithPut<T>()
        {
            return await restLibrary.RestClient.PutAsync<T>(RestRequest);
        }

        public async Task<T?> WithPatch<T>()
        {
            return await restLibrary.RestClient.PatchAsync<T>(RestRequest);
        }

        public async Task<T?> WithDelete<T>()
        {
            return await restLibrary.RestClient.DeleteAsync<T>(RestRequest);
        }

        public async Task<RestResponse> WithAsync()
        {
            return await restLibrary.RestClient.ExecuteAsync(RestRequest);
        }
    }
}
