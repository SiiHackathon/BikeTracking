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

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]

        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: News/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: News/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
