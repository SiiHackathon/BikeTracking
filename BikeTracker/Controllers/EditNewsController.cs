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
            var news = DependencyFactory.CreateNewsRepository.GetById(id);
            return View("Add", new NewsDetailsViewModel {Content = news.Content, Title = news.Title, NewsId = news.NewsId });
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(NewsDetailsViewModel model)
        {
            //NewsList.News.Add(model);
            var news = new News { AddedOn = DateTime.Now, Content = model.Content, Title = model.Title };
            DependencyFactory.CreateNewsRepository.Save(news);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            DependencyFactory.CreateNewsRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}