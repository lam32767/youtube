using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoModel;
using DataAccess;

namespace MyYouTube.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            ViewBag.Message = "View a video on your favorites";

            string thisId = (string.IsNullOrEmpty(id)) ? "wZZ7oFKsKzY" : id;
            

            MyYouTube.Models.VideoViewModel model = new Models.VideoViewModel()
            {
                ChannelTitle = "Meow channel",
                Comment = "Like",
                Dislikes = 3200,
                Likes = 100000,
                Rating = "*****",
                Title  = "Nyan cat 10 hour",
                Id = string.Empty,
                EmbedURL = thisId,
                Favorite = true,
                PublishDate = DateTime.Today.ToShortDateString()
            };
            return View(model);
        }
        public ActionResult Adder(string id)
        {
            ViewBag.Message = "Newly added video:";

            var newvid = new Video
            {
                Id = id,
                ChannelTitle = "Newvid",
                Comment = "Comment",
                Dislikes = 10,
                Likes = 11,
                PublishDate = DateTime.Now.AddMonths(-8).ToShortDateString(),
                Rating = "*****",
                Title = "Newtitle"
            };

            using (var context = new VideoContext())
            {
                context.Videos.Add(newvid);
                context.SaveChanges();
            }

            MyYouTube.Models.VideoViewModel model = new Models.VideoViewModel()
            {
                ChannelTitle = newvid.ChannelTitle,
                Comment = newvid.Comment,
                Dislikes = newvid.Dislikes,
                Likes = newvid.Likes,
                Rating = "*****",
                Title  = "Nyan cat 10 hour",
                Id = string.Empty,
                EmbedURL = id,  
                Favorite = true,
                PublishDate = DateTime.Today.ToShortDateString()
            };
            return RedirectToAction("Details",model);
        }
        public ActionResult Deleter(string id)
        {
            using (var context = new VideoContext())
            {
                Video theRecord = context.Videos.Find("wZZ7oFKsKzY");
                if (theRecord != null)
                {
                    context.Videos.Remove(theRecord);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Favorites");
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Find what you're looking for";

            return View();
        }

        public ActionResult Favorites()
        {
            ViewBag.Message = "Here's what you liked";
            using (var context = new VideoContext())
            {
                var newvid = new Video
                {
                    Id = "wZZ7oFKsKzY",
                    ChannelTitle = "Meow",
                    Comment = "Comment",
                    Dislikes = 10,
                    Likes = 11,
                    PublishDate = DateTime.Now.AddMonths(-8).ToShortDateString(),
                    Rating = "*****",
                    Title = "Nyan cat 10 hour"
                };



                List<Models.VideoViewModel> allItems = new List<Models.VideoViewModel>();

                allItems.Add(new Models.VideoViewModel()
                {
                    ChannelTitle = newvid.ChannelTitle,
                    Comment = newvid.Comment,
                    Dislikes = newvid.Dislikes,
                    Likes = newvid.Likes,
                    Rating = "*****",     ///////todo newvid.Rating,
                    Title = newvid.Title,
                    Id = string.Empty,
                    EmbedURL = newvid.Id,  ///////"wZZ7oFKsKzY"
                    Favorite = true,
                    PublishDate = DateTime.Today.ToShortDateString()
                }
                );
                return View(allItems);
            }
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
    }
}