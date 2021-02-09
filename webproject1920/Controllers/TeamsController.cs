using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webproject1920.Service.Interfaces;
using webproject1920.ViewModels;

namespace webproject1920.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IMapper _mapper;
        private ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService, IMapper mapper)
        {
            _mapper = mapper;
            _teamsService = teamsService;
        }

        public async Task<ActionResult> Index()
        {
            var list = await _teamsService.GetAll();

            List<TeamVM> listVM = _mapper.Map<List<TeamVM>>(list);

            return View(listVM);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var team = await _teamsService.GetById(id);
            if (team == null) return NotFound();

            TeamVM teamVM = _mapper.Map<TeamVM>(team);
            return View(teamVM);
        }
    }
}