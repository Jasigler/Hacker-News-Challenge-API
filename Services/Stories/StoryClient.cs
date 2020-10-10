using Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services
{
    public class StoryClient : IStoryClient 
    {
        private HttpClient _client = new HttpClient();
        private readonly string baseRoute = "https://hacker-news.firebaseio.com/v0/";
        
        public async Task<int[]> GetTopStories()
        {
            var response = await _client.GetAsync($"{baseRoute}/topstories.json");
            var storyIdArray = await response.Content.ReadFromJsonAsync<int[]>();
            return storyIdArray;
        }

        public async Task<Story> GetStoryById(int storyId)
        {
            var response = await _client.GetAsync($"{baseRoute}/item/{storyId}.json");
            var storyObject = await response.Content.ReadFromJsonAsync<Story>();
            return storyObject;
        }

    }
}
