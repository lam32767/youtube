using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideoModel;
using DataAccess;
using System.Linq;

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
                Video newvid = context.Videos.Where(x => x.Id == id).FirstOrDefault();
                Models.VideoViewModel thisitem = new Models.VideoViewModel()
                {
                    ChannelTitle = newvid.ChannelTitle,
                    Comment = newvid.Comment,
                    Dislikes = newvid.Dislikes,
                    Likes = newvid.Likes,
                    Rating = newvid.Rating,
                    Title = newvid.Title,
                    Id = newvid.Id,
                    EmbedURL = newvid.Id,
                    Favorite = true,
                    PublishDate = DateTime.Today.ToShortDateString()
                };
                
                return View(thisitem);
            }
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
                Video theRecord = context.Videos.Find(id);
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