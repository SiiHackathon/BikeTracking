using BikeTracker.Entities;
using BikeTracker.Models;
using AutoMapper;
using System.Linq;
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
        public ActionResult Edit(TeamEditModel team)
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
        
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                DependencyFactory.CreateTeamRepository.DeleteById(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
    }
}
