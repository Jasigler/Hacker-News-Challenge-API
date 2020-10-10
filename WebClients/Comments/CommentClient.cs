using Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClients.Comments
{
    public class CommentClient : ICommentClient
    {
        private readonly HttpClient _client;

        public CommentClient(HttpClient client)
        {

            _client = client;

        }
        public async Task<Comment> GetCommentById(int commentId)
        {

            return await _client.GetFromJsonAsync<Comment>($"{_client.BaseAddress}/item/{commentId}.json"); 

        }

        public async Task<List<Comment>> GetItemComments(int[] itemCommentIds)
        {
            List<Comment> requestedComments = new List<Comment>();

            foreach (var comment in itemCommentIds)
            {
                var commentObject = await _client.GetFromJsonAsync<Comment>($"{_client.BaseAddress}/item/{comment}.json");
                requestedComments.Add(commentObject);

            }

            return requestedComments;

        }
    }
}
