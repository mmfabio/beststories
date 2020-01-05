using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using beststories.Models;
using beststories.Services;

namespace beststories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IStoryService _storyService;
        private const string _storyCacheKey = "STORY_CACHE_KEY";
        private const double _timeSpam = 60;

        public ValuesController(IMemoryCache memoryCache, IStoryService storyService)
        {
            _cache = memoryCache;
            _storyService = storyService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Story>>> GetAsync()
        {
            try
            {
                if (_cache.TryGetValue(_storyCacheKey, out IEnumerable<Story> stories))
                {
                    return Ok(stories);
                }
                stories = await _storyService.getStories();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(_timeSpam));
                _cache.Set(_storyCacheKey, stories, cacheEntryOptions);
                return Ok(stories);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
