using System.Collections.Generic;
using System.Threading.Tasks;
using beststories.Models;

namespace beststories.Services
{
    public interface IStoryService
    {
        Task<IEnumerable<Story>> getStories();
    }
}