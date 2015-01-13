using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Filters;
using Scouter.Web.ViewModels;

namespace Scouter.Web.Controllers
{
    public class TeamsController : Controller
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        //
        // GET: /Teams/
        [NoCache]
        public ActionResult Index(int id = -1) //eventId
        {
            List<Team> teams = new List<Team>();

            if (id == -1)
                id = CurrentEvent();

            var q1 = from r in _unit.RobotEvents.GetAll()
                    where r.Match.FRCEvent.Id == id
                    group r by r.Team.Number into g
                    select new { TeamNo = g.Key };

            var q2 = from t in _unit.Teams.GetAll()
                     join q in q1 on t.Number equals q.TeamNo
                     orderby t.Number
                     select t;

            teams = q2.ToList();

            TeamsListViewModel vm = new TeamsListViewModel();
            vm.Teams = teams;
            return View("Index", vm);
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public sealed class NoCacheAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //filterContext.HttpContext.Response.Cache.SetNoStore(); // cannot use this because it prevents offline caching
                base.OnResultExecuting(filterContext);
            }
        }

        protected override void Dispose(bool disposing)
        {
            this._unit.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult New()
        {
            TeamViewModel vm = new TeamViewModel();
            vm.IsNew = true;

            return View("Team", vm);
        }

        [ActionName("Edit")]
        public ActionResult Get(int id)
        {
            TeamViewModel vm = new TeamViewModel();
            vm.Team = this._unit.Teams.GetById(id);

            if (vm.Team != null)
                return View("Team", vm);

            return View("NotFound");
        }

        [HttpPost()]
        public ActionResult UploadImage(HttpPostedFileBase image, int id)
        {
            JsonResult result;
            Team team;
            Random rand = new Random();
            string unique;
            string ext;
            string fileName;
            string path;

            unique = rand.Next(1000000).ToString(); // add to the filename to prevent browser cacheing

            ext = Path.GetExtension(image.FileName).ToLower();

            fileName = string.Format("{0}-{1}{2}", id, unique, ext);

            path = Path.Combine(HttpContext.Server.MapPath(Config.ImagesFolderPath), fileName);

            if (ext == ".png" || ext == ".jpg")
            {
                team = _unit.Teams.GetById(id);

                if (team != null)
                {
                    team.ImageName = fileName;
                    _unit.Teams.Update(team);
                    _unit.SaveChanges();

                    image.SaveAs(path);
                    result = this.Json(new
                        {
                            imageUrl = string.Format("{0}{1}", Config.ImagesUrlPrefix, fileName)
                        });
                }
                else
                {
                    result = this.Json(new
                        {
                            status = "error",
                            statusText = string.Format("There is no team with the Id of '{0}' in the system.", id)
                        });
                }
            }
            else
            {
                result = this.Json(new
                    {
                        status = "error",
                        statusText = "Unsupported image type. Only .png or .jpg files are acceptable."
                    });
            }

            return result;
        }

        public ActionResult Offline()
        {
            return View();
        }

        public ActionResult ReviewOffline()
        {
            return View();
        }

        public ActionResult Manifest()
        {
            this.Response.ContentType = "text/cache-manifest";
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache); // manifest must always come from the server

            return View("manifest", new ManifestViewModel(HttpContext.Server.MapPath(Config.ImagesFolderPath)));
        }
        private int CurrentEvent()
        {
            return _unit.CurrentScoutData.GetById(1).Event_ID;
        }
    }
}