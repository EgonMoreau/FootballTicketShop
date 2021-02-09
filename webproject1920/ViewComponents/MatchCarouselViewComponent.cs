using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using webproject1920.Service.Interfaces;
using webproject1920.ViewModels;

namespace webproject1920.ViewComponents
{
    public class MatchCarouselViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;

        public MatchCarouselViewComponent(IMapper mapper, ITeamsService teamsService)
        {
            _mapper = mapper;
            _teamsService = teamsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teams = await _teamsService.GetAll();
            List<TeamVM> teamsVM = _mapper.Map<List<TeamVM>>(teams);
            return View(teamsVM);
        }
    }
}
