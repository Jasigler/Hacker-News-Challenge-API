using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Http;
using WebClients.Stories;

namespace UnitTests
{
    public class TestBase
    {
        protected Mock<IMemoryCache> MockCacheClient = new Mock<IMemoryCache>();
        protected Mock<ILogger<IStoryClient>> MockLoggingClient = new Mock<ILogger<IStoryClient>>();
        protected Mock<HttpMessageHandler> MockHttpHandler = new Mock<HttpMessageHandler>();
        protected Mock<IStoryClient> MockStoryClient = new Mock<IStoryClient>();
    }
} 