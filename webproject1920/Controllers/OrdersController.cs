using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webproject1920.Domain.Entities;
using webproject1920.Service.Interfaces;

namespace webproject1920.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IOrderLineService _oLService;
        private readonly ITicketService _ticketService;

        public OrdersController(IMapper mapper, IOrderService orderService, IOrderLineService orderLineService, ISubscriptionService subscriptionService, ITicketService ticketService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _oLService = orderLineService;
            _ticketService = ticketService;
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = await _orderService.GetByCustomer(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            List<OrderVM> orderVMs = _mapper.Map<List<OrderVM>>(orders);

            return View(orderVMs);
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null || order.Customer != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))) return NotFound();

            var orderLines = await _oLService.GetByOrderId(order.Id);
            
            if (orderLines != null) orderLines = orderLines.Where(oL => oL.ReturnedAt == null).ToList();

            List<OrderLineVM> oLVMs = _mapper.Map<List<OrderLineVM>>(orderLines);

            ViewData["OrderId"] = order.Id;

            return View(oLVMs);
        }

        [HttpPost]
        public async Task<ActionResult> Return(int id)
        {
            var orderLine = await _oLService.GetById(id);
            if (orderLine == null
                || orderLine.Order == null
                || orderLine.Order.Customer != Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))) return NotFound();

            if (!orderLine.TicketId.HasValue)
            {
                ModelState.AddModelError("Return", "Enkel tickets kunnen geretourneerd worden!");
            }

            if (orderLine.ReturnedAt.HasValue)
            {
                ModelState.AddModelError("Return", "Ticket werd al geretourneerd!");
            }

            if (DateTime.Now.AddDays(7) >= orderLine.Ticket.Match.Start)
            {
                ModelState.AddModelError("Return", "Tickets kunnen maar tot 1 week voor de start van de match worden geretourneerd");
            }

            if (ModelState.IsValid)
            {
                orderLine.ReturnedAt = DateTime.Now;
                await _oLService.Update(orderLine);
                var ticket = await _ticketService.GetById(orderLine.TicketId.Value);
                if (ticket == null)
                {
                    ModelState.AddModelError("Return", "Fout!");
                    return View(_mapper.Map<OrderLineVM>(orderLine));
                }
                ticket.Valid = false;
                await _ticketService.Update(ticket);
            }

            return View(_mapper.Map<OrderLineVM>(orderLine));


        }
    }
}