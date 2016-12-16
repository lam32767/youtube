using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoModel;
using DataAccess;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyYouTube.Controllers
{
    public class HomeController : Controller
    {
        #region default nocode actions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Find what you're looking for";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.Message = "Help for users";

            return View();
        }
        public ActionResult HelpTech()
        {
            ViewBag.Message = "Help for techies";

            return View();
        }
        #endregion

        public ActionResult Details(string id)
        {
            ViewBag.Message = "View a video on your favorites";

            bool defaultVid = (string.IsNullOrEmpty(id));
            string thisId = defaultVid ? "wZZ7oFKsKzY" : id;

            if (defaultVid)
            {
                MyYouTube.Models.VideoViewModel model = new Models.VideoViewModel()
                {
                    ChannelTitle = "Default",
                    Comment = "Default",
                    Dislikes = 3,
                    Likes = 1000000,
                    Rating = "*****",
                    Title = "Nyan cat 10 hour",
                    Id = string.Empty,
                    EmbedURL = thisId,
                    Favorite = true,
                    PublishDate = DateTime.Today.ToShortDateString()
                };
                return View(model);
            }

            using (var context = new VideoContext())
            {
                Video thisRecord = context.Videos.Where(x => x.Id == id).FirstOrDefault();
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
                
                return View(thisModel);
            }
        }

        public ActionResult Adder(string sender)
        {
            try
            {
                JObject deserializedJson = (JObject)JsonConvert.DeserializeObject(sender.Replace('~', '"'));
                Models.VideoViewModel incomingModel = new Models.VideoViewModel();

                int i = 0;
                foreach (JToken token in deserializedJson.Children())
                {
                    if (token is JProperty)
                    {
                        var prop = token as JProperty;
                        if (i == 0) incomingModel.Id = prop.Value.ToString();
                        if (i == 1) incomingModel.Title = prop.Value.ToString();
                        if (i == 2) incomingModel.ChannelTitle = prop.Value.ToString();
                        if (i == 3) incomingModel.Rating = prop.Value.ToString();
                        if (i == 4) incomingModel.Comment = prop.Value.ToString();
                        if (i == 5) incomingModel.PublishDate = prop.Value.ToString();
                        if (i == 6) incomingModel.Likes = Convert.ToInt32(prop.Value.ToString());
                        if (i == 7) incomingModel.Dislikes = Convert.ToInt32(prop.Value.ToString());
                        i++;
                    }
                }

                var newvid = new Video
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

                using (var context = new VideoContext())
                {
                    context.Videos.Add(newvid);
                    context.SaveChanges();
                }

                return RedirectToAction("Details", incomingModel);
            }
            catch (Exception e )
            {
                string err = e.ToString();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Deleter(string id)
        {
            using (var context = new VideoContext())
            {
                Video theRecord = context.Videos.Find(id);
                if (theRecord != null)
                {
                    context.Videos.Remove(theRecord);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Favorites");
        }


        public ActionResult Favorites()
        {
            ViewBag.Message = "Here's what you liked";
            using (var context = new VideoContext())
            {
                List<Models.VideoViewModel> allItems = new List<Models.VideoViewModel>();
                foreach (Video eachItem in context
                                         .Videos
                                         .Where(x => x.Id != string.Empty)
                                         .ToList())
                    allItems.Add(new Models.VideoViewModel()
                    {
                        ChannelTitle = eachItem.ChannelTitle,
                        Comment = eachItem.Comment,
                        Dislikes = eachItem.Dislikes,
                        Likes = eachItem.Likes,
                        Rating = eachItem.Rating,
                        Title = eachItem.Title,
                        Id = eachItem.Id,
                        EmbedURL = eachItem.Id,  
                        Favorite = true,
                        PublishDate = DateTime.Today.ToShortDateString()
                    }
                );
                return View(allItems);
            }
        }
   }
}