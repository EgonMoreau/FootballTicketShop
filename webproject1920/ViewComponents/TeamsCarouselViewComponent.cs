using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Service;
using webproject1920.Service.Interfaces;
using webproject1920.ViewModels;

namespace webproject1920.ViewComponents
{
    public class TeamsCarouselViewComponent : ViewComponent
    {
        private readonly ITeamsService _teamsService;
        private readonly IMapper _mapper;

        public TeamsCarouselViewComponent(IMapper mapper, ITeamsService teamsService)
        {
            _teamsService = teamsService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teams = await _teamsService.GetAll();
            List<TeamVM> teamsVM = _mapper.Map<List<TeamVM>>(teams);
            return View(teamsVM);
        }
    }
}