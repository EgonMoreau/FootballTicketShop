using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using webproject1920.Service.Interfaces;
using webproject1920.ViewModels;

namespace webproject1920.ViewComponents
{
    public class TCLInfoViewComponent : ViewComponent
    {
        private readonly ITeamCompetitionLocationService _tclService;
        private readonly IMapper _mapper;

        public TCLInfoViewComponent(IMapper mapper, ITeamCompetitionLocationService teamCompetitionLocationService)
        {
            _tclService = teamCompetitionLocationService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(CartVM cartItem)
        {
            var tcl = await _tclService.GetById(cartItem.TeamSubscription.Value);
            TCLVM tclVm = _mapper.Map<TCLVM>(tcl);

            return View(tclVm);
        }
    }
}
