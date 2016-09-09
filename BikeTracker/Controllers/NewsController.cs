using System.Collections.Generic;
using BikeTracker.Models;
using System.Web.Mvc;
using System.Linq;

namespace BikeTracker.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            var news = DependencyFactory.CreateNewsRepository.GetLast(5).ToList();
            var model = news.Select(item => new NewsDetailsViewModel()
            {
                NewsId = item.Id,
                Content = item.Content,
                Title = item.Title,
                CreatedOn = item.AddedOn.ToShortDateString()
            }).ToList();
            return View(model);
        }
    }
}
