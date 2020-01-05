using System;

namespace beststories.Models
{
    public class Story
    {
        public long id { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
        public string postedBy { get; set; }
        public DateTime time { get; set; }
        public int score { get; set; }
        public int commentCount { get; set; }

        public Story(long id, string title, string uri, string postedBy, DateTime time, int score, int commentCount)
        {
            this.id = id;
            this.title = title;
            this.uri = uri;
            this.postedBy = postedBy;
            this.time = time;
            this.score = score;
            this.commentCount = commentCount;
        }
    }
}