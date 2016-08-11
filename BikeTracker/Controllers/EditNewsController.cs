using BikeTracker.Models;
using System.Web.Mvc;

namespace BikeTracker.Controllers {

    public class EditNewsController : Controller {

        [HttpGet]
        public ActionResult Index() {

            return View();

        }

        [HttpPost]
        public ActionResult Index(NewsViewModel model)
        {
            NewsList.News.Add(model);
            return View();

        }

    }
}