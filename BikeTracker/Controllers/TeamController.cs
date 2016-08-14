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
            return View(DependencyFactory.CreateTeamRepository.GetAll()
                .Select(GetViewModel)
                .ToArray());
        }

        // GET: Team/Details/5
        public ActionResult Details(long id)
        {
            var team = DependencyFactory.CreateTeamRepository.GetById(id);
            return View(new TeamDetailsViewModel
            {
                TeamId = team?.TeamId ?? 0,
                Name = team?.Name,
                Users = DependencyFactory.CreateUserRepository.GetByTeamId(id)
                    .Select(user => new TeamDetailsUserViewModel
                    {
                        UserId = user?.UserId ?? 0,
                        FirstName = user?.FirstName,
                        LastName = user?.LastName,
                        TotalDistance = DependencyFactory.CreateActivityService.GetUserTotalDistance(id)
                    })
                    .ToArray()
            });
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamEditModel team)
        {
            return Save(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(long id)
        {
            return View(GetById(id));
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(TeamEditModel team)
        {
            return Save(team);
        }

        private ActionResult Save(TeamEditModel team)
        {
            try
            {
                DependencyFactory.CreateTeamRepository.Save(new Team
                {
                    TeamId = team.TeamId,
                    Name = team.Name
                });

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
            return View(GetViewModel(DependencyFactory.CreateTeamRepository.GetById(id)));
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(long teamId, FormCollection collection)
        {
            try
            {
                DependencyFactory.CreateTeamRepository.DeleteById(teamId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static TeamEditModel GetById(long teamId)
        {
            var team = DependencyFactory.CreateTeamRepository.GetById(teamId);
            return new TeamEditModel
            {
                TeamId = team?.TeamId ?? 0,
                Name = team?.Name
            };
        }

        private static TeamViewModel GetViewModel(Team team)
        {
            return new TeamViewModel
            {
                TeamId = team?.TeamId ?? 0,
                Name = team?.Name
            };
        }
    }
}
