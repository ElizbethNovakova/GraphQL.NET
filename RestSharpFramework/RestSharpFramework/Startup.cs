using Microsoft.Extensions.DependencyInjection;
using RestSharpFramework.Base;

namespace RestSharpFramework
{
    public class Startup 
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IRestLibrary>(new RestLibrary(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<GraphQLProductApp.Startup>()))
                .AddScoped<IRestFactory, RestFactory>()
                .AddScoped<IRestBuilder, RestBuilder>();
        }
    }
}
