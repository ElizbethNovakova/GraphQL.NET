using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTesting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddSingleton<IGraphQLClient>(new GraphQLHttpClient(
                new Uri("https://localhost:5001/graphql"), 
                new NewtonsoftJsonSerializer()));
        }
    }
}
