using System;
using VideoModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyYouTube
{
    public class Mapper
    {

        public static Models.VideoViewModel MakeDefaultModel(string Id)
        {
            Models.VideoViewModel model = new Models.VideoViewModel()
            {
                ChannelTitle = "Default",
                Comment = "Default",
                Dislikes = 3,
                Likes = 1000000,
                Rating = "*****",
                Title = "Nyan cat 10 hour",
                Id = string.Empty,
                EmbedURL = Id,
                Favorite = true,
                PublishDate = DateTime.Today.ToShortDateString()
            };
            return model;
        }
        public static Models.VideoViewModel MakeModelFromRecord(Video thisRecord)
        {
            Models.VideoViewModel thisModel = new Models.VideoViewModel()
            {
                ChannelTitle = thisRecord.ChannelTitle,
                Comment = thisRecord.Comment,
                Dislikes = thisRecord.Dislikes,
                Likes = thisRecord.Likes,
                Rating = thisRecord.Rating,
                Title = thisRecord.Title,
                Id = thisRecord.Id,
                EmbedURL = thisRecord.Id,
                Favorite = true,
                PublishDate = DateTime.Today.ToShortDateString()
            };
            return thisModel;
        }
        public static Models.VideoViewModel MakeModelFromString(string sender)
        {
            JObject deserializedJson = (JObject)JsonConvert.DeserializeObject(sender.Replace('~', '"'));
            Models.VideoViewModel retval = new Models.VideoViewModel();

            int i = 0;
            foreach (JToken token in deserializedJson.Children())
            {
                if (token is JProperty)
                {
                    var prop = token as JProperty;
                    if (i == 0) retval.Id = prop.Value.ToString();
                    if (i == 1) retval.Title = prop.Value.ToString();
                    if (i == 2) retval.ChannelTitle = prop.Value.ToString();
                    if (i == 3) retval.Rating = prop.Value.ToString();
                    if (i == 4) retval.Comment = prop.Value.ToString();
                    if (i == 5) retval.PublishDate = prop.Value.ToString();
                    if (i == 6) retval.Likes = Convert.ToInt32(prop.Value.ToString());
                    if (i == 7) retval.Dislikes = Convert.ToInt32(prop.Value.ToString());
                    i++;
                }
            }
            return retval;
        }
        public static Video MakeRecordFromViewModel(Models.VideoViewModel incomingModel)
        {
            Video retval = new Video()
            {
                Id = incomingModel.Id,
                ChannelTitle = incomingModel.ChannelTitle,
                Comment = incomingModel.Comment,
                Dislikes = incomingModel.Dislikes,
                Likes = incomingModel.Likes,
                PublishDate = incomingModel.PublishDate,
                Rating = incomingModel.Rating,
                Title = incomingModel.Title
            };

            return retval;
        }
    }
}