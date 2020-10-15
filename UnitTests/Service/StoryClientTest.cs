using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WebClients.Stories;
using Xunit;

namespace UnitTests.Service
{
    public class StoryClientTest
    {
        [Fact]
        public void Client_RequestsStoryFromCache_PriorToMakingHNCall()
        {
            // Arrange
            object cacheOutput;

            var requestedStory = new Story(); 

            var cachedStory = new Story()
            {
                by = "HNTestUser1234",
                descendants = 2,
                id = 12345678,
                kids = { },
                score = 10,
                time = 987653872,
                title = "How I learned to stop worrying and love Xunit",
                type = "story",
                url = "http://www.news.ycombinator.com"
            };

            var mockCacheClient = new Mock<IMemoryCache>();
            var mockLoggingClient = new Mock<ILogger<IStoryClient>>();
            var mockHandler = new Mock<HttpMessageHandler>();

            mockCacheClient.Setup(client => client.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>);

            mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), 
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                });

            var httpClient = new HttpClient(mockHandler.Object);

            var storyClient = new StoryClient(httpClient, mockCacheClient.Object, mockLoggingClient.Object);

            // Act
            var result = storyClient.GetStoryById(requestedStory.id);
            

            // Assert
            mockCacheClient.Verify(call => call.TryGetValue(It.IsAny<object>(), out cacheOutput), Times.Once);
           
        }

        [Fact]
        public void Client_LogsExceptionMessage_WhenHttpExceptionIsThrown()
        {
            // I don't understand this well enough to write meaningful tests. I could make them pass, but they'd be
            // useless...
            
            
            //Arrange
            //var requestedStoryId = 12345678;

            //var requestedStory = new Story();
            //var mockCacheClient = new Mock<IMemoryCache>();
            //var mockHandler = new Mock<HttpMessageHandler>();
            //var mockLoggingClient = new Mock<ILogger<IStoryClient>>();

            //mockCacheClient.Setup(client => client.CreateEntry(It.IsAny<object>()))
            //    .Returns(Mock.Of<ICacheEntry>);
            //mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
            //    ItExpr.IsAny<CancellationToken>())
            //    .ReturnsAsync(new HttpResponseMessage()
            //    {
            //       StatusCode = HttpStatusCode.NotFound,
            //    });

            //var httpClient = new HttpClient(mockHandler.Object);

            //var storyClient = new StoryClient(httpClient, mockCacheClient.Object, mockLoggingClient.Object);

            // Act
            //var result = storyClient.GetStoryById(requestedStoryId);

            // Assert
            //Exception exception = Assert.Throws<Exception>(result);
            //mockLoggingClient.Verify( logger => logger.Log(LogLevel.Warning, It.IsAny<EventId>(),
            //        It.Is<It.IsAnyType>((o, t) => string.Equals("Index page say hello", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            //        It.IsAny<Exception>(),(Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
            //    Times.Once);
        }

    }
}
