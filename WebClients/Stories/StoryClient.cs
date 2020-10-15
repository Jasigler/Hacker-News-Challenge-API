using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClients.Stories
{
    public class StoryClient : IStoryClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private IMemoryCache _cache;

        public StoryClient(HttpClient client, IMemoryCache cache, ILogger<IStoryClient> logger)
        {
            _client = client;
            _cache = cache;
            _logger = logger;
        }
        public async Task<Story> GetStoryById(int storyId)
        {
            var requestedStory = new Story();

            if (!_cache.TryGetValue(storyId, out requestedStory))
            {
                try
                {
                    requestedStory = await _client.GetFromJsonAsync<Story>($"{_client.BaseAddress}/item/{storyId}.json");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                }
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                
                _cache.CreateEntry(requestedStory);
            }

            return requestedStory;
            

        }

        public async Task<int[]> GetNewStoryIds()
        {
            int[] newStories = { };

            try
            {
                newStories = await _client.GetFromJsonAsync<int[]>($"{_client.BaseAddress}/newstories.json");
            }

            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

            return newStories;
        }
    }
}
