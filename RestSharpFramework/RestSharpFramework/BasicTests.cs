using RestSharp;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using GraphQLProductApp.Data;
using GraphQLProductApp.Controllers;
using System.Net;
using RestSharpFramework.Base;

namespace RestSharpFramework
{
    public class BasicTests
    {
        private readonly IRestFactory restFactory;
        private readonly string? token;
        public BasicTests(IRestFactory restFactory)
        {
            this.restFactory = restFactory;
            token = GetToken();
        }

        private  string GetToken()
        {
            var authResponse = restFactory
                .Create()
                .WithRequest("api/Authenticate/Login")
                .WithBody(new LoginModel()
                {
                    UserName = "KK",
                    Password = "123456"
                })
                .WithPost().Result.Content;

            return JObject.Parse(authResponse)["token"].ToString();
        }

        [Fact]
        public async Task GetOperationTest()
        {
            
            var response = await restFactory.Create()
                .WithRequest("/Product/GetProductById/1")
                .WithHeader("Authorization", $"Bearer {token}")
                .WithGet<Product>();

            response?.Name.Should().Be("Keyboard");
        }

        [Fact]
        public async Task GetQuerySegment()
        {
            var response = await restFactory.Create()
               .WithRequest("/Product/GetProductById/{id}")
               .WithHeader("Authorization", $"Bearer {token}")
               .WithUrlSegment("id", "1")
               .WithGet<Product>();

            response?.Name.Should().Be("Keyboard");
        }

        [Fact]
        public async Task GetQueryParameters()
        {
            var response = await restFactory.Create()
               .WithRequest("/Product/GetProductByIdAndName")
               .WithHeader("Authorization", $"Bearer {token}")
               .WithQueryParameter("id", "2")
               .WithQueryParameter("name", "Monitor")
               .WithGet<Product>();

            response?.Price.Should().Be(400);
        }

        [Fact]
        public async Task PostProductTest()
        {
            var response = await restFactory.Create()
               .WithRequest("/Product/Create")
               .WithHeader("Authorization", $"Bearer {token}")
               .WithBody(new Product
               {
                   Name = "Printer",
                   Description = "Color printer",
                   Price = 500,
                   ProductType = ProductType.EXTERNAL
               })
               .WithPost<Product>();

            response?.Price.Should().Be(500);
        }

        [Fact]
        public async Task FileUploadTest()
        {
            var response = await restFactory.Create()
               .WithRequest("/Product", Method.Post)
               .WithHeader("Authorization", $"Bearer {token}")
               .WithFile("myFile", @"C:\Users\ynova\Pictures\UAC.png", "multipart/form-data")
               .WithAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}