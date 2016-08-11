using BikeTracker.Entities;
using BikeTracker.Models;
using BikeTracker.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace BikeTracker.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            return View(RepositoryFactory.CreateTeamRepository.GetAll().Select(ConvertToViewModel).ToArray());
        }

        // GET: Team/Details/5
        public ActionResult Details(long id)
        {
            return View(ConvertToViewModel(RepositoryFactory.CreateTeamRepository.GetById(id)));
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamViewModel team)
        {
            try
            {
                RepositoryFactory.CreateTeamRepository.Save(new Team
                {
                    Name = team.Name
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Edit/5
        public ActionResult Edit(long id)
        {
            return View();
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(TeamViewModel team)
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

        // GET: Team/Delete/5
        public ActionResult Delete(long id)
        {
            return View();
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, FormCollection collection)
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

        private TeamViewModel ConvertToViewModel(Team team)
        {
            return new TeamViewModel
            {
                TeamId = team?.TeamId ?? 0,
                Name = team?.Name
            };
        }
    }
}
