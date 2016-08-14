using BikeTracker.Entities;
using BikeTracker.Models;
using System.Linq;
using System.Web.Mvc;

namespace BikeTracker.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(DependencyFactory.CreateUserRepository.GetAll()
                .Select(GetViewModel)
                .ToArray());
        }

        // GET: User/Details/5
        public ActionResult Details(long id)
        {
            var user = DependencyFactory.CreateUserRepository.GetById(id);
            return View(new UserDetailsViewModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                TeamName = GetTeamName(user),
                TotalDistance = DependencyFactory.CreateActivityService.GetUserTotalDistance(id),
                Activities = DependencyFactory.CreateActivityRepository.GetByUserId(id)
                    .Select(activity => new UserDetailsActivityViewModel
                    {
                        ActivityId = activity.ActivityId,
                        ActivityDate = activity.ActivityDate,
                        Distance = activity.Distance
                    })
                    .ToArray()
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
                DependencyFactory.CreateUserRepository.Save(new User
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
            return View(GetViewModel(DependencyFactory.CreateUserRepository.GetById(id)));
        }

        // POST: User/Delete/5
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

        private static UserEditModel GetById(long userId)
        {
            var user = DependencyFactory.CreateUserRepository.GetById(userId);
            return new UserEditModel
            {
                UserId = user?.UserId ?? 0,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                TeamId = user?.TeamId ?? 0,
                AvailableTeams = new[] { new SelectListItem { Value = string.Empty, Text = string.Empty } }.Concat(
                    DependencyFactory.CreateTeamRepository.GetAll()
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
            return DependencyFactory.CreateTeamRepository.GetById(user?.TeamId ?? 0)?.Name;
        }
    }
}
