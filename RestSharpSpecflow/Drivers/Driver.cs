using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpSpecflow.Drivers
{
    public class Driver
    {
        public Driver(ScenarioContext scenarioContext)
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001/"),
                RemoteCertificateValidationCallback =
                    (sender, certificate, chain, errors) => true
            };

            var restClient = new RestClient(restClientOptions);
            scenarioContext.Add("RestClient", restClient);
        }
    }
}
