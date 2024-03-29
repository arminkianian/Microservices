using PactNet;
using PactNet.Mocks.MockHttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.PactTests
{
    public class ConsumerPactClassFixture : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get { return 9222; } }
        public string MockProviderServiceBaseUri { get { return $"http://localhost:{MockServerPort}"; } }

        private bool _disposeValue=false;

        public ConsumerPactClassFixture()
        {
            var pactConfig = new PactConfig()
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"..\..\..\..\..\pacts",
                LogDir = @".\pact_logs"
            };

            PactBuilder = new PactBuilder(pactConfig);

            PactBuilder.ServiceConsumer("Consumer").HasPactWith("Provider");

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposeValue)
            {
                if (disposing)
                {
                    PactBuilder.Build();
                }

                _disposeValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
