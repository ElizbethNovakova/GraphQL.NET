using FluentAssertions;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLProductApp.Data;

namespace GraphQLTesting
{
    public class UnitTest1
    {
        private readonly IGraphQLClient graphQLClient;
        public UnitTest1(IGraphQLClient graphQLClient)
        {
            this.graphQLClient = graphQLClient;
        }

        [Fact]
        public async Task Test1Async()
        {
            var query = new GraphQLRequest
            {
                Query = @"{
                              products{
                                name,
                                price,
                                components{
                                  id,
                                  name
                                }
                              }
                            }
                        "
            };

            var response = await graphQLClient.SendQueryAsync<ProductQueryResponse>(query);
            response.Data.Products.Should().Contain(x => x.Name == "Keyboard");
        }

        public class ProductQueryResponse
        {
            public IEnumerable<Product> Products { get; set; }
        }
    }
}