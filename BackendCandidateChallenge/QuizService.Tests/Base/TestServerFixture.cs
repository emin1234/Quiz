using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuizService.Tests.Base
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                   .UseStartup<Startup>(); 

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
        }
        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
