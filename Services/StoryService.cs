using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Linq;
using System.IO;
using System;
using beststories.Models;

namespace beststories.Services
{
    public class StoryService : IStoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public StoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private async Task<IEnumerable<long>> getIds()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://hacker-news.firebaseio.com/v0/beststories.json");
            HttpClient client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                IEnumerable<long> ids = await JsonSerializer.DeserializeAsync<IEnumerable<long>>(responseStream);
                return ids;
            }
            throw new System.Exception();
        }

        private DateTime getFormatedDate(long timestamp)
        {
            DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }

        public async Task<IEnumerable<Story>> getStories()
        {
            List<Story> stories = new List<Story>();
            foreach(long id in await getIds())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                HttpClient client = _clientFactory.CreateClient();
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                    OriginalStory originalStory = await JsonSerializer.DeserializeAsync<OriginalStory>(responseStream);
                    stories.Add(new Story(
                            originalStory.id,
                            originalStory.title,
                            originalStory.url,
                            originalStory.by,
                            getFormatedDate(originalStory.time),
                            originalStory.score,
                            originalStory.descendants
                        )
                    );
                }
                else
                {
                    throw new System.Exception();
                }
            }
            return stories.OrderByDescending(s => s.score).Take(20);
        }
    }
}