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
        /// <summary>
        /// Default ctor will use the database
        /// </summary>
        public HomeController() { }

        /// <summary>
        /// Ctor that receives a db context [used in mock testing]
        /// </summary>
        /// <param name="context">Mocked or real db context</param>
        public HomeController(IVideoContext context) { _DBContext = context; }

        private const string HanselmanVideoId = "H2KkiRbDZyc";
        private IVideoContext _DBContext = new VideoContext();

        #region default nocode actions; nothing to see here
        /// <summary>
        /// The home page; it displays all links. 
        /// All links are also available as menu items.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The search page where user will find videos on youtube, 
        /// page through them, and choose to add to favorites or 
        /// view on youtube.com.
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            ViewBag.Message = "Find what you're looking for";
            return View();
        }

        /// <summary>
        /// User help on how to use the system.
        /// </summary>
        /// <returns></returns>
        public ActionResult Help()
        {
            ViewBag.Message = "Help for users";
            return View();
        }

        /// <summary>
        /// Technical help page.  Normally the user would not see this, 
        /// but for the purposes of this project, it is viewable.  
        /// </summary>
        /// <returns></returns>
        public ActionResult HelpTech()
        {
            ViewBag.Message = "Help for techies";
            return View();
        }
        #endregion

        /// <summary>
        /// View one video chosen from the favorites page.
        /// If no id is handed in, [i.e. string.empty or null], 
        ///    view a default Hanselman video.
        /// </summary>
        /// <param name="id">Primary key for video. Equal to the youtube generated random character 'key'.</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Message = "View the default Hanselman video";
                return View(Mapper.MakeDefaultModel(HanselmanVideoId));
            }

            try
            {
                Video thisRecord = _DBContext
                                    .GetVideos()
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                if (thisRecord != null)
                {
                    ViewBag.Message = "View a video from your favorites";
                    return View(Mapper.MakeModelFromRecord(thisRecord));
                }
            }
            catch (Exception e)
            {
                string err = e.ToString();
                _DBContext.LogError(e);
            }

            /////if we fall down to here something didn't work
            ViewBag.Message = "View the default Hanselman video; requested video does not exist";
            return View(Mapper.MakeDefaultModel(HanselmanVideoId));
        }

        /// <summary>
        /// List the favorites that the user has added to collection. 
        /// Links are available for viewing the videos and removing from 
        /// the favorites collection.
        /// </summary>
        /// <returns></returns>
        public ActionResult Favorites()
        {
            List<Models.VideoViewModel> allItems = new List<Models.VideoViewModel>();
            try
            {
                foreach (Video eachItem in _DBContext
                                         .GetVideos()
                                         .Where(x => x.Id != string.Empty)
                                         .ToList())
                {
                    allItems.Add(Mapper.MakeModelFromRecord(eachItem));
                }
            }
            catch (Exception e)
            {
                string err = e.ToString();
                _DBContext.LogError(e);
            }

            ViewBag.Message = "Here's what you liked";
            return View(allItems);
        }

        /// <summary>
        /// Add selected video to the data store. 
        /// </summary>
        /// <param name="sender">Json string that represents the view model</param>
        /// <returns></returns>
        public ActionResult Adder(string sender)
        {
            try
            {
                Models.VideoViewModel incomingModel = Mapper.MakeModelFromString(sender);

                _DBContext.Videos.Add(Mapper.MakeRecordFromViewModel(incomingModel));
                _DBContext.SaveChanges();

                return RedirectToAction("Details", incomingModel);
            }
            catch (Exception e)
            {
                string err = e.ToString();
                _DBContext.LogError(e);
            }

            ////if we fall down to here, something didn't work
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Delete selected video from data store.
        /// </summary>
        /// <param name="id">Primary key for the selected video</param>
        /// <returns></returns>
        public ActionResult Deleter(string id)
        {
            try
            {
                Video theRecord = _DBContext.FindVideoById(id);
                if (theRecord != null)
                {
                    _DBContext.Videos.Remove(theRecord);
                    _DBContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                string err = e.ToString();
                _DBContext.LogError(e);
            }
            return RedirectToAction("Favorites");
        }

    }
}