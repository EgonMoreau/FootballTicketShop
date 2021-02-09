using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using webproject1920.Service.Interfaces;
using webproject1920.ViewModels;

namespace webproject1920.ViewComponents
{
    public class MSLInfoViewComponent : ViewComponent
    {
        private readonly IMatchStadiumLocationService _mslService;
        private readonly IMapper _mapper;

        public MSLInfoViewComponent(IMapper mapper, IMatchStadiumLocationService mslService)
        {
            _mslService = mslService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(CartVM cartItem)
        {
            var msl = await _mslService.GetById(cartItem.MatchTicket.Value);
            MSLVM mslVM = _mapper.Map<MSLVM>(msl);

            return View(new CartTicketInfoVM
            {
                MSLVM = mslVM,
                Count = cartItem.Count
            });
        }
    }
}
