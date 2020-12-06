using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebClients.Stories;
using Xunit;

namespace UnitTests.Service
{
    public class StoryClientTest: TestBase
    {
        [Fact]
        public async Task Client_RequestsStoryFromCache_PriorToMakingHNCall()
        {
            // Arrange
            object cacheOutput;

            var requestedStory = new Story();

            MockCacheClient.Setup(client => client.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>);

            MockHttpHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                });

            var httpClient = new HttpClient(MockHttpHandler.Object);

            var storyClient = new StoryClient(httpClient, MockCacheClient.Object, MockLoggingClient.Object);

            // Act
            await storyClient.GetStoryById(requestedStory.id);

            // Assert
            MockCacheClient.Verify(call => call.TryGetValue(It.IsAny<object>(), out cacheOutput), Times.Once);

        }

        [Fact]
        public async Task Client_LogsExceptionMessage_WhenHttpExceptionIsThrown()
        {
            //Arrange
            var requestedStoryId = 12345678;


            MockCacheClient.Setup(client => client.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>);
            MockHttpHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .Throws(new HttpRequestException());

            var httpClient = new HttpClient(MockHttpHandler.Object);

            var storyClient = new StoryClient(httpClient, MockCacheClient.Object, MockLoggingClient.Object);

            // Act
            await storyClient.GetStoryById(requestedStoryId);

            // Assert
            MockLoggingClient.Verify(log => log.Log(
              It.IsAny<LogLevel>(),
              It.IsAny<EventId>(),
              It.Is<It.IsAnyType>((o, t) => true),
              It.IsAny<Exception>(),
              It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)));
        }

    }
}
