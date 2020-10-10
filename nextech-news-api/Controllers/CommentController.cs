using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebClients.Comments;

namespace nextech_news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentClient _client;

        public CommentController(ICommentClient client)
        {
            _client = client;

        }


        [HttpGet("{commentId}")]
        public async Task<ActionResult<Comment>> GetCommentDataById([FromRoute] int commentId)
        {
            var requestedCommentObject = await _client.GetCommentById(commentId);

            if (requestedCommentObject != null)
            {
                return Ok(requestedCommentObject);

            }
            else return NotFound("No comment matching that Id found.");
            
        }
        
    }
}
