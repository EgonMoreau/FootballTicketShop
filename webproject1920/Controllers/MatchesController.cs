using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Service;
using webproject1920.Service.Interfaces;
using webproject1920.ViewModels;

namespace webproject1920.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ITeamsService _teamService;
        private readonly IMatchStadiumLocationService _mslService;
        private readonly IMapper _mapper;

        public MatchesController(IMapper mapper, IMatchService matchService, IMatchStadiumLocationService mslService, ITeamsService teamsService)
        {
            _matchService = matchService;
            _teamService = teamsService;
            _mapper = mapper;
            _mslService = mslService;
        }

        public async Task<ActionResult> Index(MatchCalendarVM vm)
        {
            if (vm.TeamId.HasValue)
            {
                var team = await _teamService.GetById(vm.TeamId.Value);
                if (team == null) return NotFound();
                vm.Team = _mapper.Map<TeamVM>(team);
            }
            return View("Calendar", vm);
        }

        public async Task<ActionResult> Details(int id)
        {
            var match = await _matchService.GetById(id);
            if (match == null) return NotFound();

            var msl = await _mslService.GetByMatchId(match.Id);
            var mslVM = _mapper.Map<List<MSLVM>>(msl);
            var matchVM = _mapper.Map<MatchVM>(match);
            var stadiumVM = _mapper.Map<StadiumVM>(match.Stadium);
            var detailInfo = new MatchInfo()
            {
                MSL = mslVM,
                Match = matchVM,
                Stadium = stadiumVM
            };

            return View(detailInfo);
        }     

        [HttpPost]
        public async Task<IEnumerable<MatchCalendarDataVM>> CalendarData(CalendarReturnDataVM calendarData)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            try
            {
                DateTime start = DateTime.Parse(calendarData.Start);
                DateTime end = DateTime.Parse(calendarData.End);

                IEnumerable<Match> list;

                if (!calendarData.TeamId.HasValue)
                {
                    list = await _matchService.GetAll(start, end);
                } else
                {
                    list = await _matchService.GetByTeamId(start, end, calendarData.TeamId.Value);
                }

                List<MatchCalendarDataVM> listVM = _mapper.Map<List<MatchCalendarDataVM>>(list);
                return listVM;
            }
            catch (Exception e)
            {
                Debug.WriteLine("MatchesController: " + e.Message);
                return null;
            }

        }
    }
}