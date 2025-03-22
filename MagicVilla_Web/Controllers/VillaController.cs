using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MagicVilla_Web.Controllers
{
    public class VillaController:  Controller
    {
        private readonly IVillaService _VillaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper) 
        {
            _VillaService = villaService;
            _mapper = mapper;                                                                                                                                                                                                               

            
        }

        public async  Task<IActionResult> Index()
        {
           
            var response = await _VillaService.GetAllAsync();          
            return View(response);
        }

    }
}
