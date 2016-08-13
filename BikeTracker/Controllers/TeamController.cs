using BikeTracker.Entities;
using BikeTracker.Models;
using BikeTracker.Repositories;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BikeTracker.Controllers
{
    public class TeamController : Controller
    {
        private ITeamRepository _teamRepository;

        public TeamController()
        {
            _teamRepository = RepositoryFactory.CreateTeamRepository;
        }

        public ActionResult Index()
        {
            return View(_teamRepository.GetAll().Select(Mapper.Map<TeamViewModel>));
        }

        public ActionResult Details(long id)
        {
            return View(Mapper.Map<TeamViewModel>(_teamRepository.GetById(id)));
        }
        
        [Authorize]
        public ActionResult List()
        {
            return View(_teamRepository.GetAll().Select(Mapper.Map<TeamViewModel>));
        }
        
        [Authorize]
        public ActionResult Edit(long id)
        {
            Team team = (id == 0) ? new Team() : _teamRepository.GetById(id);
            return View(Mapper.Map<TeamViewModel>(team));
        }

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

        [Authorize]
        [HttpPost]
        public ActionResult Delete(long id)
        {
            _teamRepository.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}
