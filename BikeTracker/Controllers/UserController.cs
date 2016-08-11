using BikeTracker.Entities;
using BikeTracker.Models;
using BikeTracker.Repositories;
using System.Linq;
using System.Web.Mvc;
using System;

namespace BikeTracker.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(RepositoryFactory.CreateUserRepository.GetAll().Select(ConvertToViewModel).ToArray());
        }

        // GET: User/Details/5
        public ActionResult Details(long id)
        {
            return View(ConvertToViewModel(RepositoryFactory.CreateUserRepository.GetById(id)));
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            try
            {
                RepositoryFactory.CreateUserRepository.Save(new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(long id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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

        private UserViewModel ConvertToViewModel(User user)
        {
            return new UserViewModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName
            };
        }
    }
}
