using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClients.Stories
{
    public class StoryClient : IStoryClient
    {
        private readonly HttpClient _client;

        public StoryClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<Story> GetStoryById(int storyId)
        {
            return await _client.GetFromJsonAsync<Story>($"{_client.BaseAddress}/item/{storyId}.json");
        }

        public async Task<int[]> GetNewStoryIds()
        {
           return await _client.GetFromJsonAsync<int[]>($"{_client.BaseAddress}/newstories.json");

        }
    }
}
