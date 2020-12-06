using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using nextech_news_api.Controllers;
using WebClients.Stories;
using Xunit;
namespace UnitTests.Controller
{
    public class StoryControllerTest: TestBase
    {

        [Fact]
        public void GetNewStories_Returns_ArrayOfStories()
        {
            // Arrange
            int[] idArray = { 12345678, 87654321, 09876543 };

            MockStoryClient.Setup(client => client.GetNewStoryIds())
                .ReturnsAsync(idArray);

            var storyController = new StoryController(MockStoryClient.Object);

            // Act
            var result = storyController.GetNewStories().Result;

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetNewStories_Returns_NotFound_WhenIdArrayIsEmpty()
        {
            // Arrange
            int[] idArray = { };
            
            MockStoryClient.Setup(client => client.GetNewStoryIds())
                .ReturnsAsync(idArray);

            var storyController = new StoryController(MockStoryClient.Object);

            // Act
             var result = storyController.GetNewStories().Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetStory_Returns_RequestedStory()
        {
            // Arrange
            Story testStory = new Story
            {
                by = "Hacker1223435",
                descendants = 23,
                id = 12345678,
                kids = { },
                score = 12,
                time = 324890234,
                title = "Story",
                type = "story",
                url = "https://www.google.com"
            };

            MockStoryClient.Setup(client => client.GetStoryById(It.IsAny<int>()))
                .ReturnsAsync(testStory);

            var storyController = new StoryController(MockStoryClient.Object);

            // Act
            var result = storyController.GetStoryById(testStory.id).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetStory_Returns_NotFoundResult_WhenNullObjectIsReceived()
        {
            // Arrange
            MockStoryClient.Setup(client => client.GetStoryById(It.IsAny<int>()));

            var storyController = new StoryController(MockStoryClient.Object);

            // Act
            var result = storyController.GetStoryById(1234).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
