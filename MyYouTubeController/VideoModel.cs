namespace MyYouTube.Models
{
    public class VideoViewModel : VideoModel.Video
    {
        public string PublishDateString { get { return PublishDate.ToShortDateString();  } }

        public string EmbedURL { get; set; }
        public string URL { get { return "https://www.youtube.com/embed/" + EmbedURL; } }

        public bool Favorite { get; set; }
        
        public string UnFaveLink
        {
            get
            {
                return "<a href='/home/Deleter/" + Id + "'>Remove</a>";
            }
        }

    }
}