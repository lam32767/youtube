using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoModel;

namespace MyYouTube.Models
{
    public class VideoViewModel : VideoModel.Video
    {
        public string PublishDateString { get { return PublishDate;  } }

        public string EmbedURL { get; set; }
        public string URL { get { return "https://www.youtube.com/embed/" + EmbedURL; } }

        public bool Favorite { get; set; }

        // &#10004; for a checkmark using html dot raw
        public string FavoriteString { get { return (Favorite) ? "X" : string.Empty; } }

        public string UnFaveLink
        {
            get
            {
                return "<a href='/home/Deleter/" + Id + "'>Remove</a>";
            }
        }

    }
}