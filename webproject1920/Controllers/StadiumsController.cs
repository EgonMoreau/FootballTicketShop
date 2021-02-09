using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StadiumsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStadiumService _stadionService;

        public StadiumsController(IMapper mapper, IStadiumService stadionService)
        {
            _mapper = mapper;
            _stadionService = stadionService;
        }

        public async Task<ActionResult> Index()
        {
            var list = await _stadionService.GetAll();
            List<StadiumVM> listVM = _mapper.Map<List<StadiumVM>>(list);
            return View(listVM);
        }
        
        public async Task<ActionResult> Details(int id)
        {
            var stadium = await _stadionService.GetById(id);
            if (stadium == null) return NotFound();

            StadiumVM stadiumVM = _mapper.Map<StadiumVM>(stadium);
            return View(stadiumVM);
        }
    }
}