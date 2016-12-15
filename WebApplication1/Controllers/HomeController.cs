using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyYouTube.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details( string id)
        {
            ViewBag.Message = "Here's your video:";
            if (string.IsNullOrEmpty(id))
            {
                id = "wZZ7oFKsKzY" ;
            }

            MyYouTube.Models.VideoModel model = new Models.VideoModel()
            {
                Channel = "Meow channel",
                Comments = "Like",
                DislikeCount = 3200,
                LikeCount = 100000,
                Rating = 5,
                VideoTitle = "Nyan cat 10 hour",
                Id = 0,
                EmbedURL = id,  ///////"wZZ7oFKsKzY"
                Favorite = true,
                AddDate = DateTime.Today
        };
            return View(model);
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Find what you're looking for";

            return View();
        }

        public ActionResult Favorites()
        {

            DateTime x = DateTime.Today;
            ViewBag.Message = "Here's what you liked";

            List<Models.VideoModel> allItems = new List<Models.VideoModel>();

            for (int y=0;y<100;y++)
            allItems.Add(
                new Models.VideoModel()
                {
                    Channel = "Meow channel",
                    Comments = string.Format("Like {0}",y.ToString("0000")),
                    DislikeCount = 3200,
                    LikeCount = 100000,
                    Rating = 5,
                    VideoTitle = string.Format("Title {0}", y.ToString("0000")),
                    Id = 0,
                    EmbedURL = "wZZ7oFKsKzY",
                    Favorite = true,
                    AddDate = DateTime.Today
                });


            return View(allItems);
        }
        public ActionResult Help()
        {
            ViewBag.Message = "Help";

            return View();
        }
    }
}