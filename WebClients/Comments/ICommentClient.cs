using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebClients.Comments
{
    public interface ICommentClient
    {
        public Task<Comment> GetCommentById(int commentId);
    }
}
