using System;

namespace VideoModel
{
    public class Video
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Rating { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string ChannelTitle { get; set; }
        public string Comment { get; set; }
        public DateTime PublishDate { get; set; }
    }
}