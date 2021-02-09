using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Service.Interfaces;
using webproject1920.Util;
using webproject1920.Util.Mail;
using webproject1920.ViewModels;

namespace webproject1920.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private const string KEY_SHOPPINGCART = "ShoppingCart";

        private readonly IMapper _mapper;
        private readonly IMatchService _matchService;
        private readonly IMatchStadiumLocationService _mslService;
        private readonly ITeamsService _teamsService;
        private readonly ICompetitionsService _competitionsService;
        private readonly ILocationsService _locationsService;
        private readonly ITicketService _ticketService;
        private readonly IOrderLineService _orderLineService;
        private readonly IOrderService _orderService;
        private readonly ITeamCompetitionLocationService _tclService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IEmailSender _emailSender;

        public ShopController(IMapper mapper, IMatchService matchService, ITeamsService teamsService, IMatchStadiumLocationService mslService
            , ICompetitionsService competitionsService, ILocationsService locationsService, IOrderService orderService, IOrderLineService orderLineService,
            ITicketService ticketService, ITeamCompetitionLocationService tclService, ISubscriptionService subscriptionService, IEmailSender emailSender)
        {
            _mapper = mapper;
            _matchService = matchService;
            _teamsService = teamsService;
            _mslService = mslService;
            _competitionsService = competitionsService;
            _locationsService = locationsService;
            _orderLineService = orderLineService;
            _orderService = orderService;
            _ticketService = ticketService;
            _tclService = tclService;
            _subscriptionService = subscriptionService;
            _emailSender = emailSender;
        }

        public ActionResult Index()
        {
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<ActionResult> Cart()
        {
            ShoppingCartVM cart;
            cart = HttpContext.Session.GetObject<ShoppingCartVM>(KEY_SHOPPINGCART);
            if (cart == null)
            {
                cart = new ShoppingCartVM
                {
                    Cart = new List<CartVM>()
                };
                HttpContext.Session.SetObject(KEY_SHOPPINGCART, cart);
            }

            if (TempData["CartError"] != null)
            {
                ModelState.AddModelError("Cart", TempData["CartError"].ToString());
                TempData["CartError"] = null;
            }

            cart.TotalPrice = 0.0;
            foreach (var item in cart.Cart)
            {
                if (item.MatchTicket.HasValue)
                {
                    var msl = await _mslService.GetById(item.MatchTicket.Value);

                    if (item.Count > msl.RemainingSeats)
                    {
                        item.Count = msl.RemainingSeats;
                        ModelState.AddModelError("Cart", "Niet genoeg beschikbare plaatsen voor 1 van de wedstrijden. Aantal werd aangepast.");
                    }
                    cart.TotalPrice += msl.Price * item.Count;
                }
                else if (item.TeamSubscription.HasValue) {
                    var tcl = await _tclService.GetById(item.TeamSubscription.Value);
                    cart.TotalPrice += tcl.Price;
                }
            }
            HttpContext.Session.SetObject(KEY_SHOPPINGCART, cart);
            
            return View(cart);
        }

        [HttpPost]
        public ActionResult Cart(CartVM vm)
        {
            ShoppingCartVM cart;
            cart = HttpContext.Session.GetObject<ShoppingCartVM>(KEY_SHOPPINGCART);
            if (cart == null)
            {
                cart = new ShoppingCartVM
                {
                    Cart = new List<CartVM>()
                };
                HttpContext.Session.SetObject(KEY_SHOPPINGCART, cart);
            }

            if (!ModelState.IsValid) RedirectToAction(nameof(Cart));

            if (vm.MatchTicket.HasValue && vm.ForMatchId.HasValue)
            {
                // Added match ticket
                if (vm.Count < 1)
                {
                    cart.Cart.RemoveAll(item => item.MatchTicket == vm.MatchTicket);
                }
                else if (cart.Cart.Any(x => x.MatchTicket == vm.MatchTicket))
                {
                    cart.Cart.Where(x => x.MatchTicket == vm.MatchTicket).ToList().ForEach(x => x.Count = vm.Count);
                }
                else
                {
                    cart.Cart.Add(vm);
                }

                int matchTicketCount = cart.Cart.Where(x => x.ForMatchId == vm.ForMatchId).Select(x => x.Count).Sum();
                if (matchTicketCount > 10)
                {
                    cart.Cart.Find(x => x.MatchTicket == vm.MatchTicket).Count = vm.Count - (matchTicketCount - 10);
                    TempData["CartError"] = "Maximum 10 tickets per match toegelaten!";
                }
            }
            else if (vm.TeamSubscription.HasValue && vm.ForTeamId.HasValue)
            {
                // Added subscription

                if (vm.Count < 1)
                {
                    cart.Cart.RemoveAll(item => item.TeamSubscription == vm.TeamSubscription);
                }
                else if (cart.Cart.Any(x => x.TeamSubscription == vm.TeamSubscription))
                {
                    cart.Cart.Where(x => x.TeamSubscription == vm.TeamSubscription).ToList().ForEach(x => x.Count = vm.Count);
                }
                else
                {
                    cart.Cart.Add(vm);
                }
            }
            HttpContext.Session.SetObject(KEY_SHOPPINGCART, cart);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public ActionResult RemoveCart(CartVM vm)
        {
            ShoppingCartVM cart = HttpContext.Session.GetObject<ShoppingCartVM>(KEY_SHOPPINGCART);
            if (cart == null) return NotFound();

            if (vm.MatchTicket.HasValue)
            {
                // Is ticket
                cart.Cart.RemoveAll(x => x.MatchTicket == vm.MatchTicket);
            }
            else if (vm.TeamSubscription.HasValue)
            {
                // Is subscription
                cart.Cart.RemoveAll(x => x.TeamSubscription == vm.TeamSubscription);
            }

            HttpContext.Session.SetObject(KEY_SHOPPINGCART, cart);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public ActionResult EmptyCart()
        {
            ShoppingCartVM cart = HttpContext.Session.GetObject<ShoppingCartVM>(KEY_SHOPPINGCART);
            if (cart == null) return NotFound();

            cart.Cart = new List<CartVM>();

            HttpContext.Session.SetObject(KEY_SHOPPINGCART, cart);

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Ticket(int matchId)
        {
            var match = await _matchService.GetById(matchId);
            if (match == null) return NotFound();

            if (DateTime.Now >= match.Start || DateTime.Now.AddMonths(1) < match.Start.Date) return NotFound();

            var msls = await _mslService.GetByMatchId(matchId);
            var mslsVM = _mapper.Map<List<MSLVM>>(msls);

            var matchVM = _mapper.Map<MatchVM>(match);
            var stadiumVM = _mapper.Map<StadiumVM>(match.Stadium);
            var shopInfo = new ShopTicketInfoVM()
            {
                MSLs = mslsVM,
                Match = matchVM,
                Stadium = stadiumVM
            };

            return View(shopInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Ticket(int matchId, int mslId)
        {
            var match = await _matchService.GetById(matchId);
            if (match == null) return NotFound();
            var matchVM = _mapper.Map<MatchVM>(match);

            if (DateTime.Now >= match.Start || DateTime.Now.AddMonths(1) < match.Start.Date) return NotFound();

            var selectedMsl = await _mslService.GetById(mslId);
            var selectedMslVM = _mapper.Map<MSLVM>(selectedMsl);

            var msls = await _mslService.GetByMatchId(matchId);
            var mslsVM = _mapper.Map<List<MSLVM>>(msls);

            var stadiumVM = _mapper.Map<StadiumVM>(match.Stadium);
            var shopInfo = new ShopTicketInfoVM()
            {
                MSLs = mslsVM,
                SelectedMSL = selectedMslVM,
                Match = matchVM,
                Stadium = stadiumVM
            };

            return View(shopInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Subscription(int teamId)
        {
            var team = await _teamsService.GetById(teamId);
            var competition = await _competitionsService.GetNextCompetition(DateTime.Now);
            var locations = await _locationsService.GetAll();

            var competitionVm = _mapper.Map<CompetitionVM>(competition);
            var locationVm = _mapper.Map<List<LocationVM>>(locations);

            var tcls = await _tclService.GetByTeamCompetitionId(team.Id, competition.Id);
            var tclVm = _mapper.Map<List<TCLVM>>(tcls);

            var teamVm = _mapper.Map<TeamVM>(team);

            var shopSubInfo = new ShopSubscriptionVM()
            {
                Team = teamVm,
                Tcl = tclVm,
                Competition = competitionVm,
                Locations = locationVm

            };

            return View(shopSubInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Subscription(int teamId, int tclId)
        {

            var competition = await _competitionsService.GetNextCompetition(DateTime.Now);
            var locations = await _locationsService.GetAll();
            var team = await _teamsService.GetById(teamId);
            var selectedLocation = await _tclService.GetById(tclId);


            var locationVm = _mapper.Map<List<LocationVM>>(locations);
            var competitionVm = _mapper.Map<CompetitionVM>(competition);
            var selectedLocationVm = _mapper.Map<TCLVM>(selectedLocation);
            var teamVm = _mapper.Map<TeamVM>(team);

            var tcls = await _tclService.GetByTeamCompetitionId(team.Id, competition.Id);
            var tclVm = _mapper.Map<List<TCLVM>>(tcls);

            var shopSubInfo = new ShopSubscriptionVM()
            {
                Team = teamVm,
                Tcl = tclVm,
                SelectedTcl = selectedLocationVm,
                Competition = competitionVm,
                Locations = locationVm
            };

            return View(shopSubInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Confirm()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            ShoppingCartVM cart;
            cart = HttpContext.Session.GetObject<ShoppingCartVM>(KEY_SHOPPINGCART);
            if (cart == null) return NotFound();

            var newOrder = new Order
            {
                Customer = Guid.Parse(userId)
            };
            var newOrderLines = new List<NewOrderLineVM>();

            foreach (var item in cart.Cart)
            {
                if (item.MatchTicket.HasValue)
                {
                    // New ticket
                    var msl = await _mslService.GetById(item.MatchTicket.Value);

                    if (DateTime.Now >= msl.Match.Start || DateTime.Now.AddMonths(1) < msl.Match.Start.Date)
                    {
                        TempData["CartError"] = "Probleem met winkelmandje!";
                        cart.Cart.Remove(item);
                        return RedirectToAction(nameof(Cart));
                    }

                    var previousMatchTickets = await _ticketService.GetByCustomerAndMatch(Guid.Parse(userId), msl.MatchId);
                    int prevMatchTicketCount = 0;
                    if (previousMatchTickets != null) prevMatchTicketCount = previousMatchTickets.Count();

                    var totalTicketCount = cart.Cart.Where(x => x.ForMatchId == item.ForMatchId).Select(x => x.Count).Sum() + prevMatchTicketCount;

                    if (item.Count <= msl.RemainingSeats && totalTicketCount <= 10)
                    {
                        for (int i = 0; i < item.Count; i++)
                        {
                            newOrderLines.Add(new NewOrderLineVM
                            {
                                MSL = item.MatchTicket,
                                Match = msl.MatchId
                            });
                        }
                    }
                    else
                    {
                        TempData["CartError"] = $"Maximum 10 tickets per match toegelaten! U bestelde al {prevMatchTicketCount} tickets voor {msl.Match.TeamHomeNavigation.Name} - {msl.Match.TeamAwayNavigation.Name} op {msl.Match.Start.ToShortDateString()}! Gelieve uw winkelmandje aan te passen";
                        return RedirectToAction(nameof(Cart));
                    }

                }
                else if (item.TeamSubscription.HasValue)
                {
                    // New subscription
                    newOrderLines.Add(new NewOrderLineVM
                    {
                        TSL = item.TeamSubscription
                    });
                }
            }

            if (newOrderLines.Count < 1)
            {
                TempData["CartError"] = "Probleem met winkelmandje!";
                return RedirectToAction(nameof(Cart));
            }

            await _orderService.Create(newOrder);
            List<string> orderLineEmail = new List<string>();
            foreach (var newOL in newOrderLines)
            {
                if (newOL.MSL.HasValue && newOL.Match.HasValue)
                {
                    // Is ticket
                    var msl = await _mslService.GetById(newOL.MSL.Value);
                    msl.RemainingSeats--;
                    await _mslService.Update(msl);

                    var newTicket = new Ticket
                    {
                        MatchStadiumLocationId = newOL.MSL.Value,
                        MatchId = newOL.Match.Value,
                        Customer = Guid.Parse(userId)
                    };
                    await _ticketService.Create(newTicket);

                    await _orderLineService.Create(new OrderLine
                    {
                        TicketId = newTicket.Id,
                        OrderId = newOrder.Id,
                        Price = msl.Price
                    });

                    orderLineEmail.Add($"<p>Ticket: {newTicket.Code}</p> <p>Match: {msl.Match.TeamHomeNavigation.Name} - {msl.Match.TeamAwayNavigation.Name}</p> <p>Start: {msl.Match.Start.ToShortDateString()} {msl.Match.Start.ToShortTimeString()}</p> <p>Plaats:  {msl.StadiumLocation.Location.Location}</p>");

                }
                else if (newOL.TSL.HasValue)
                {
                    // Is subscription

                    var tsl = await _tclService.GetById(newOL.TSL.Value);
                    var matches = await _matchService.GetByCompetitionTeamId(tsl.CompetitionId, tsl.TeamId);
                    var msls = await _mslService.GetMslByMatches(matches);

                    foreach (var msl in msls.Where(x => x.LocationId == tsl.LocationId).ToList())
                    {
                        if (msl.RemainingSeats < 1)
                        {
                            TempData["CartError"] = $"Kies een andere zitplaats aub. De zitplaatsen voor {tsl.Location.Location} zijn uitverkocht";
                            return RedirectToAction(nameof(Cart));
                        }
                        
                    }

                    var newSubscription = new Subscription
                    {
                        TeamCompetitionLocation = tsl,
                        Customer = Guid.Parse(userId)
                    };

                    await _subscriptionService.Create(newSubscription);

                    await _orderLineService.Create(new OrderLine
                    {
                        SubscriptionId = newSubscription.Id,
                        OrderId = newOrder.Id,
                        Price = tsl.Price
                    });

                    foreach (var msl in msls.Where(x => x.LocationId == tsl.LocationId).ToList())
                    {
                        msl.RemainingSeats--;
                        await _mslService.Update(msl);
                    }

                    orderLineEmail.Add($"<p>Ticket: {newSubscription.Code}</p> <p>Subscrition: {tsl.Team.Name} from {tsl.Competition.StartDate} until {tsl.Competition.EndDate}</p> <p>Plaats:  {tsl.Location.Location}</p>");

                }
            }

            await _emailSender.SendEmailOrderAsync(userEmail, $"Ticketverkoop: order confirmation {newOrder.Id}", $"<h1> Order confirmation Order Id: {newOrder.Id} </h1>", orderLineEmail.ToArray());

            TempData["LatestOrderId"] = newOrder.Id;
            cart.Cart = new List<CartVM>();
            HttpContext.Session.SetObject(KEY_SHOPPINGCART, cart);
            return RedirectToAction("Details" , "Orders", new { id = newOrder.Id });
        }
    }
}