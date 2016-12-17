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
        public HomeController() { }
        public HomeController(IVideoContext context)
        {
            db = context;
        }
        private const string NyanCatVideoId = "wZZ7oFKsKzY";
        private IVideoContext db = new VideoContext();

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
                //using (var context = new VideoContext())
                using (var context = db)
                {
                    Video thisRecord = context.Videos.Where(x => x.Id == id).FirstOrDefault();
                    Models.VideoViewModel thisModel = Mapper.MakeModelFromRecord(thisRecord);
                    ViewBag.Message = "View a video from your favorites";
                    return View(thisModel);
                }
        }

        public ActionResult Favorites()
        {
            ViewBag.Message = "Here's what you liked";
            using (var context = db)
            //using (var context = new VideoContext())
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

        public ActionResult Adder(string sender)
        {
            try
            {
                Models.VideoViewModel incomingModel = Mapper.MakeModelFromString(sender);

                //using (var context = new VideoContext())
                using (var context = db)
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
            //using (var context = new VideoContext())
            using (var context = db)
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

   }
}