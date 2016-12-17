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
        private const string NyanCatVideoId = "wZZ7oFKsKzY";

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
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Message = "View the default nyan cat video";
                return View(Mapper.MakeDefaultModel(NyanCatVideoId));
            }
            else
                using (var context = new VideoContext())
                {
                    Video thisRecord = context.Videos.Where(x => x.Id == id).FirstOrDefault();
                    Models.VideoViewModel thisModel = Mapper.MakeModelFromRecord(thisRecord);
                    ViewBag.Message = "View a video from your favorites";
                    return View(thisModel);
                }
        }


        public ActionResult Adder(string sender)
        {
            try
            {
                Models.VideoViewModel incomingModel = Mapper.MakeModelFromString(sender);

                using (var context = new VideoContext())
                {
                    context.Videos.Add(Mapper.MakeRecordFromViewModel(incomingModel));
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
                    allItems.Add(Mapper.MakeModelFromRecord(eachItem));
                return View(allItems);
            }
        }
   }
}