using Models;
using System.Threading.Tasks;


namespace WebClients.Stories
{
    public interface IStoryClient 
    {
        public Task<int[]> GetNewStoryIds();
        public Task<Story> GetStoryById(int storyId);
    }
}
