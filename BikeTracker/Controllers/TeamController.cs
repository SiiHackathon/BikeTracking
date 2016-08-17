using BikeTracker.Entities;
using BikeTracker.Models;
using AutoMapper;
using System.Linq;
using System.IO;
using System.Web.Mvc;

namespace BikeTracker.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(DependencyFactory.CreateTeamRepository.GetAll()
                .Select(Mapper.Map<TeamViewModel>));
        }
        
        public ActionResult List()
        {
            return View(DependencyFactory.CreateTeamRepository.GetAll()
                .Select(Mapper.Map<TeamViewModel>));
        }

        [AllowAnonymous]
        public ActionResult Details(long id)
        {
            var team = DependencyFactory.CreateTeamRepository.GetById(id);
            return View(new TeamDetailsViewModel
            {
                TeamId = team?.TeamId ?? 0,
                Name = team?.Name,
                Image = team?.Image,
                ReverseRoute = team?.ReverseRoute ?? false,
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
                
        public ActionResult Edit(long id)
        {
            var model = (id == 0) ?
                new TeamEditModel() :
                Mapper.Map<TeamEditModel>(DependencyFactory.CreateTeamRepository.GetById(id));
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamEditModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                Team team = (model.TeamId == 0) ?
                    new Team() :
                    DependencyFactory.CreateTeamRepository.GetById(model.TeamId);
                Mapper.Map(model, team);

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/Teams"), fileName);
                        file.SaveAs(path);
                        team.Image = $"~/Images/Teams/{fileName}";
                    }
                }

                DependencyFactory.CreateTeamRepository.Save(team);

                return RedirectToAction("List");
            }
            catch
            {
                return View(model);
            }
        }
        
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                DependencyFactory.CreateTeamRepository.DeleteById(id);

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

		[AllowAnonymous]
		public PartialViewResult TeamStandings(long distance = 0)
		{
			var teamRepo = DependencyFactory.CreateTeamRepository;
			var teams = teamRepo.GetAll()
				.Select(m => new TeamStandingsViewModel{
					CurrentDistance = (decimal)m.CurrentDistance / 1000,
					DistanceToGo = (decimal)(distance - m.CurrentDistance)  / 1000,
					FinishedLaps = m.TracksCompleted,
					Name = m.Name,
					TeamId = m.TeamId
				});
			return PartialView("_TeamStandings", teams);
		}		
	}
}
