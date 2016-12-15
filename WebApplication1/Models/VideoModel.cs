using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyYouTube.Models
{
    public class VideoModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string VideoTitle { get; set; }

        [Display(Name = "Channel")]
        public string Channel { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Likes")]
        public int LikeCount { get; set; }

        [Display(Name = "Dislikes")]
        public int DislikeCount { get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }

        public string RatingString
        { get
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < Rating; i++)
                    sb.Append("*");
                return sb.ToString();
            }
        }

        public DateTime AddDate { get; set; }

        public string DateString { get { return AddDate.ToString("yyyy/MM/dd");  } }

        public string EmbedURL { get; set; }
        public string URL { get { return "https://www.youtube.com/embed/" + EmbedURL; } }

        public bool Favorite { get; set; }

        // &#10004; for a checkmark using html dot raw
        public string FavoriteString { get { return (Favorite) ? "X" : string.Empty; } }


    }
}