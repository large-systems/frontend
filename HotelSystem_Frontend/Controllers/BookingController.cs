using System.Collections.Generic;
using HotelInterface.Interface;
using HotelInterface.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using HotelSystem.Exception;

namespace HotelSystem_Frontend.Controllers
{
    [Route("Booking")]
    public class BookingController : Controller
    {

        private readonly IServiceHotel _hotelClient;

        public BookingController(IServiceHotel hotelClient)
        {
            _hotelClient = hotelClient;
        }


        [Route("")]
        public IActionResult Index([FromQuery(Name = "passportNumber")]string passportNumber = null)
        {
            List<BookingDetails> bookings = null;
            if (passportNumber != null)
            {
                if (int.TryParse(passportNumber, out int number))
                {
                    bookings = _hotelClient.FindBookings(number);
                }
                else
                {
                    ViewData["error"] = "Passport number must be a number";
                }
            }
            return View(bookings);
        }

        [Route("{id}")]
        public IActionResult Details([FromRoute(Name ="id")]int id)
        {
            BookingDetails details = null;
            try
            {
                details = _hotelClient.FindBookingByid(new BookingIdentifier(id));
            }
            catch (FaultException<BookingNotFoundException>)
            {
                TempData["error"] = "Booking could not be found";
            }
            if (details == null)
            {
                if (!TempData.ContainsKey("error")) TempData["error"] = "Booking not found";
                return RedirectToAction("Index");
            }
            return View(details);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
    }
}