using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpFramework.Base
{
    public interface IRestFactory
    {
        IRestBuilder Create();
    }

    public class RestFactory : IRestFactory
    {
        private readonly IRestBuilder restBuilder;

        public RestFactory(IRestBuilder restBuilder)
        {
            this.restBuilder = restBuilder;
        }

        public IRestBuilder Create()
        {
            return restBuilder;
        }
    }
}
