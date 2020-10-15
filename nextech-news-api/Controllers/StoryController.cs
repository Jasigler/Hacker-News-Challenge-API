using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models;
using WebClients.Stories;

namespace nextech_news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryClient _client;

        public StoryController(IStoryClient client)
        {
            _client = client;
            
        }
        
        [HttpGet("new")]
        public async Task<ActionResult<int[]>> GetNewStories()
        {
            
            var topStoryList = await _client.GetNewStoryIds();

            if (topStoryList.Length > 0)
            {
                return Ok(topStoryList);
            }

            else return NotFound("No stories found.");
        }

        [HttpGet("{storyId}")]
        public async Task<ActionResult<Story>> GetStoryById([FromRoute] int storyId)
        {

            var requestedStory = await _client.GetStoryById(storyId);

            if (requestedStory != null)
            {
                return Ok(requestedStory);
            }
            else return NotFound();
        }
    }
}
