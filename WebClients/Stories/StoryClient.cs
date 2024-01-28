using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClients.Stories
{
    public class StoryClient : IStoryClient, IDisposable
    {
        private readonly HttpClient Client;
        private readonly ILogger Logger;
        private IMemoryCache Cache;

        public StoryClient(HttpClient client, IMemoryCache cache, ILogger<IStoryClient> logger)
        {
            Client = client;
            Cache = cache;
            Logger = logger;
        }
        public async Task<Story> GetStoryById(int storyId)
        {
            Story requestedStory;
            
            if (!Cache.TryGetValue(storyId, out requestedStory))
            {
                try
                {
                   requestedStory = await Client.GetFromJsonAsync<Story>($"{Client.BaseAddress}/item/{storyId}.json");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                }
                
               Cache.CreateEntry(requestedStory);
            }

            return requestedStory;
            

        }

        public async Task<int[]> GetNewStoryIds()
        {

            return await Client.GetFromJsonAsync<int[]>($"{Client.BaseAddress}/newstories.json");
        }

        // Explaining git and a little programming to my daughter
        public bool AmIDaphne(string name)
        {
            bool amIDaphne = false;

            if (name.ToLower() == "daphne")
            {
                amIDaphne = true;
            }

            return amIDaphne;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Reset();
        }

        public void Reset()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                Reset();
            }
        }

        
    }
}
