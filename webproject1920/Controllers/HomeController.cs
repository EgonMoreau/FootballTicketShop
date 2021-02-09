using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webproject1920.Domain.Entities;
using webproject1920.Models;
using webproject1920.Service;
using webproject1920.Service.Interfaces;
using webproject1920.Util.Mail;
using webproject1920.ViewModels;

namespace webproject1920.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;

        public HomeController(IEmailSender emailSender, ITeamsService teamsService, IMapper mapper)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            _teamsService = teamsService;
        }

        public async Task<ActionResult> Index()
        {
            var list = await _teamsService.GetAll();

            List<TeamVM> listVM = _mapper.Map<List<TeamVM>>(list);

            return View(listVM);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(SendMailVM sendMailVM)
        {

            _emailSender.SendEmailAsync(sendMailVM.FromEmail, "contact pagina", sendMailVM.Message);
            return View();
        }
        
    }
}
