using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelInterface.DTOs;
using HotelInterface.Interface;
using HotelSystem_Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem_Frontend.Controllers
{
    public class HotelChainController : Controller
    {

        private readonly IServiceHotel _hotelClient;

        public HotelChainController(IServiceHotel hotelClient)
        {
            _hotelClient = hotelClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(HotelModel hotelModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Index");
            }
            _hotelClient.AddNewHotel(hotelModel.Name, hotelModel.City, hotelModel.Address, new HotelChainIdentifier(hotelModel.HotelChainIdentifier)); //Maybe tryParse?
            TempData["message"] = $"Successfully created Hotel with an identifier: {hotelModel.HotelChainIdentifier}";
            return RedirectToAction("Index");
        }
    }
}