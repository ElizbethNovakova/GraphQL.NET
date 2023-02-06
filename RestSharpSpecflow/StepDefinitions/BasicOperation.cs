using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace RestSharpSpecflow.StepDefinitions
{
    [Binding]
    public  class BasicOperation
    {
        private readonly ScenarioContext scenarioContext;
        private Product? restResponse;
        private RestClient restClient;

        public BasicOperation(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            restClient = scenarioContext.Get<RestClient>("RestClient");
        }

        [Given(@"I perform a GET operation of ""(.*)")]
        public async Task GivenIPerformAGetOperationOfAsync(string path, Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            var token = GetToken();

            var request = new RestRequest(path);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddUrlSegment("id", (int)data.ProductId);
            restResponse = restClient.Get<Product>(request);
        }

        [Given(@"I should get the product name as ""(.*)")]
        public void GivenIShouldGetTheProductNameAs(string value)
        {
            restResponse?.Name.Should().Be(value);
        }

        private string GetToken()
        {
            var authRequest = new RestRequest("api/Authenticate/Login");
            authRequest.AddJsonBody(new LoginModel()
            {
                UserName = "KK",
                Password = "123456"
            });
            var authResponse = restClient.PostAsync(authRequest).Result.Content;
            
            return JObject.Parse(authResponse)["token"].ToString();
        }
    }
}
