using System;
using System.Linq;
using BikeTracker.Models;
using System.Web.Mvc;
using BikeTracker.Entities;

namespace BikeTracker.Controllers
{
    [Authorize]
    public class EditNewsController : Controller
    {
        public ActionResult Index()
        {
            var news = DependencyFactory.CreateNewsRepository.GetAll().Select(x => new NewsViewModel { NewsId = x.NewsId, Title = x.Title });
            return View(news);
        }

        public ActionResult Edit(long id)
        {
            var news = DependencyFactory.CreateNewsRepository.GetById(id) ?? new News { AddedOn = DateTime.Now };
            return View("Edit", new NewsDetailsViewModel {Content = news.Content, Title = news.Title, NewsId = news.NewsId, AddedOn = news.AddedOn });
        }

        [HttpGet]
        public ActionResult Add()
        {
            return RedirectToAction("Edit",new { id = 0 });
        }

        [HttpPost]
        public ActionResult Edit(NewsDetailsViewModel model)
        {
            var news = new News { NewsId = model.NewsId, AddedOn = model.AddedOn, Content = model.Content, Title = model.Title };
            DependencyFactory.CreateNewsRepository.Save(news);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            DependencyFactory.CreateNewsRepository.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}