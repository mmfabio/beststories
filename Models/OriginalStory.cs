namespace beststories.Models
{
    public class OriginalStory
    {
        public long id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string by { get; set; }
        public long time { get; set; }
        public int score { get; set; }
        public int descendants { get; set; }
        public string type { get; set; }
    }
}