using RestSharpSpecflow.Drivers;

namespace RestSharpSpecflow.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext scenarioContext;
        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario()]
        public void InitializeDriver()
        {
            Driver driver = new Driver(scenarioContext);
        }
    }
}
