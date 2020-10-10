using Models;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services
{
    public class CommentClient : ICommentClient
    {
        private HttpClient _client = new HttpClient();
        private readonly string baseRoute = "https://hacker-news.firebase.io/com/v0/";
        
            public async Task<Comment> GetCommentById(int commentId)
        {
            var response = await _client.GetAsync($"{baseRoute}/item/{commentId}.json");
            var commentObject = await response.Content.ReadFromJsonAsync<Comment>();
            return commentObject;
        }


    }
}
