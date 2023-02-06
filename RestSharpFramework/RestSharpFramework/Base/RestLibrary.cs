using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;

namespace RestSharpFramework.Base
{
    public interface IRestLibrary
    {
        RestClient RestClient { get; }
    }

    public class RestLibrary : IRestLibrary
    {
        public RestLibrary(WebApplicationFactory<GraphQLProductApp.Startup> webApplicationFactory)
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001/"),
                RemoteCertificateValidationCallback =
                    (sender, certificate, chain, errors) => true
            };
            
            var client = webApplicationFactory.CreateDefaultClient();
            RestClient = new RestClient(client, restClientOptions);
        }

        public RestClient RestClient { get; }
    }
}
