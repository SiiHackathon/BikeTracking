using System.Collections.Generic;
using BikeTracker.Models;
using System.Web.Mvc;
using System.Linq;

namespace BikeTracker.Controllers
{
    public class NewsController : Controller
    {
        private const int numberOfNewsOnPage = 3;
        // GET: News
        public ActionResult Index(bool showAll = false)
        {
            var news = DependencyFactory.CreateNewsRepository.GetLast(showAll ? (int?)null : numberOfNewsOnPage).ToList();
            var model = news.Select(item => new NewsDetailsViewModel()
            {
                NewsId = item.Id,
                Content = item.Content,
                Title = item.Title,
                AddedOn = item.AddedOn
            }).ToList();
            return View(model);
        }
    }
}
