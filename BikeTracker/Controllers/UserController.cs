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
            return View(RepositoryFactory.CreateUserRepository.GetAll()
                .Select(GetViewModel)
                .ToArray());
        }

        // GET: User/Details/5
        public ActionResult Details(long id)
        {
            var user = RepositoryFactory.CreateUserRepository.GetById(id);
            return View(new UserDetailsViewModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                TeamName = GetTeamName(user),
                TotalDistance = 35,
                Activities = new[]
                {
                    new UserDetailsActivityViewModel
                    {
                        ActivityId = 1,
                        ActivityDate = new DateTime(2016, 8, 12),
                        Distance = 9.5M
                    },
                    new UserDetailsActivityViewModel
                    {
                        ActivityId = 2,
                        ActivityDate = new DateTime(2016, 8, 13),
                        Distance = 25.5M
                    }
                }
            });
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserEditModel user)
        {
            return Save(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(long id)
        {
            return View(GetById(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserEditModel user)
        {
            return Save(user);
        }

        private ActionResult Save(UserEditModel user)
        {
            try
            {
                RepositoryFactory.CreateUserRepository.Save(new User
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TeamId = user.TeamId
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(long id)
        {
            return View(GetViewModel(RepositoryFactory.CreateUserRepository.GetById(id)));
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(long teamId, FormCollection collection)
        {
            try
            {
                RepositoryFactory.CreateTeamRepository.DeleteById(teamId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static UserEditModel GetById(long userId)
        {
            var user = RepositoryFactory.CreateUserRepository.GetById(userId);
            return new UserEditModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                TeamId = user?.TeamId ?? 0,
                AvailableTeams = new[] { new SelectListItem { Value = string.Empty, Text = string.Empty } }.Concat(
                    RepositoryFactory.CreateTeamRepository.GetAll()
                        .Select(team => new SelectListItem { Value = team.TeamId.ToString(), Text = team.Name })
                        .ToArray())
            };
        }

        private static UserViewModel GetViewModel(User user)
        {
            return new UserViewModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                TeamName = GetTeamName(user),
                TotalDistance = 35
            };
        }

        private static string GetTeamName(User user)
        {
            return RepositoryFactory.CreateTeamRepository.GetById(user?.TeamId ?? 0)?.Name;
        }
    }
}
