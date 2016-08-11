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
            return View(GetById(id));
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View(GetById(0));
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            try
            {
                RepositoryFactory.CreateUserRepository.Save(ConvertToEntity(user));

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
            return View(GetById(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            try
            {
                RepositoryFactory.CreateUserRepository.Save(ConvertToEntity(user));

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

        private UserViewModel GetById(long userId)
        {
            var user = ConvertToViewModel(RepositoryFactory.CreateUserRepository.GetById(userId));
            user.AvailableTeams = RepositoryFactory.CreateTeamRepository.GetAll()
                .Select(team => new SelectListItem { Value = team.TeamId.ToString(), Text = team.Name }).ToArray();
            return user;
        }

        private UserViewModel ConvertToViewModel(User user)
        {
            return new UserViewModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                TeamId = user?.TeamId ?? 0,
                TeamName = RepositoryFactory.CreateTeamRepository.GetById(user?.TeamId ?? 0)?.Name
            };
        }

        private User ConvertToEntity(UserViewModel user)
        {
            return new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TeamId = user.TeamId
            };
        }
    }
}
