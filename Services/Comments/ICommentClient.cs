﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface ICommentClient
    {
        public Task<Comment> GetCommentById(int commentId);

    }
}
