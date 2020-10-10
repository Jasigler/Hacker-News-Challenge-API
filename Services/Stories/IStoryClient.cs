using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IStoryClient
    {
        public Task<int[]> GetTopStories();
        public Task<Story> GetStoryById(int storyId);

    }
}
