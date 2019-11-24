using System;
using System.Collections.Generic;
using HotelInterface.Interface;
using HotelInterface.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using HotelSystem.Exception;
using System.Globalization;
using System.Runtime.InteropServices;
using HotelDetails = HotelSystem_Frontend.Models.HotelDetails;

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
    public IActionResult Details([FromRoute(Name = "id")]int id)
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

    [Route("cancel/{id}")]
    public IActionResult Create([FromRoute(Name = "id")]int id)
    {
      try
      {
        _hotelClient.CancelBooking(new BookingIdentifier(id));
        TempData["success"] = "Booking was cancelled successfully";
      }
      catch (FaultException e) when
          (e is FaultException<BookingNotFoundException>
          || e is FaultException<BookingCancelledAllreadyException>)
      {
        TempData["error"] = "Booking not found or already cancelled";

      }
      return RedirectToAction("Index");
    }

    [Route("SearchAvailability")]
    public IActionResult SearchAvailability(string city, string checkin_date, string checkout_date, string numberGuest, string numberOfRooms)
    {
      List<HotelInterface.DTOs.HotelDetails> hotels = null;

      if (!string.IsNullOrEmpty(checkout_date))
      {
        DateTime startDate = DateTime.ParseExact(checkin_date, "dd-MM-yyyy", CultureInfo.InvariantCulture); ;
        DateTime endDate = DateTime.ParseExact(checkout_date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        int numberRooms = Int32.Parse(numberOfRooms);

        TempData["startDate"] = startDate;
        TempData["endDate"] = endDate;
        TempData["numberGuest"] = numberGuest;
        TempData["numberRooms"] = numberRooms;
        hotels = _hotelClient.FindAvailableHotels(startDate, endDate, numberRooms, city);
      }
      return View(hotels);
    }

    [Route("Book")]
    public IActionResult Book(int id)
    {
      var rooms = GetRoomFromHotel(id);

      TempData["hotelId"] = id;
      TempData.Keep();

      return View(rooms);
    }

    [Route("CreateBooking")]
    [HttpGet]
    public IActionResult CreateBooking(int id)
    {
      TempData["roomId"] = id;

      TempData.Keep();
      return View();
    }
    [Route("CreateBooking")]
    [HttpPost]
    public IActionResult CreateBooking(int passportNumber, string guestNumber)
    {
      DateTime startDate = DateTime.Now;
      DateTime endDate = DateTime.Now;
      int numberRooms = 0;
      int numberGuest = 0;
      int hotelId = 0;
      int roomId = 0;
      List<RoomIdentifier> listOfRooms = null;

      if (passportNumber > 0 && !string.IsNullOrEmpty(guestNumber))
      {
        if (TempData["hotelId"] != null)
          hotelId = Convert.ToInt32(TempData["hotelId"]);

        if (TempData["roomId"] != null)
          roomId = Convert.ToInt32(TempData["roomId"]);

        if (TempData["startDate"] != null)
          startDate = Convert.ToDateTime(TempData["startDate"]);

        if (TempData["endDate"] != null)
          endDate = Convert.ToDateTime(TempData["endDate"]);

        if (TempData["numberGuest"] != null)
          numberGuest = Convert.ToInt32(TempData["numberGuest"]);

        if (TempData["numberRooms"] != null)
          numberRooms = Convert.ToInt32(TempData["numberRooms"]);

        listOfRooms = new List<RoomIdentifier>()
        {
          new RoomIdentifier(roomId),
        };

        if (!ModelState.IsValid)
          return View();
        else
        {
          _hotelClient.CreateBooking(startDate, endDate, numberGuest, listOfRooms, passportNumber, guestNumber);
          TempData["success_added"] = "Booking  added successfully!";
          return RedirectToAction("Index");
        }
      }

      return View();
    }

    private static Dictionary<int, List<RoomDetails>> GetAllRooms()
    {
      var richmondRoomsDetails = new List<RoomDetails>()
      {
         new RoomDetails(704){
           ID = 704,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        },
         new RoomDetails(705){
           ID = 702,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        },
         new RoomDetails(706){
           ID = 706,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        }
      };
      var marinaRoomsDetails = new List<RoomDetails>()
      {
         new RoomDetails(1584){
           ID = 1584,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        } ,
        new RoomDetails(1012){
           ID = 1012,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        },
           new RoomDetails(1010){
           ID = 1010,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        }
      };
      var cabinRoomsDetails = new List<RoomDetails>()
      {
            new RoomDetails(904){
           ID = 904,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        },
             new RoomDetails(905){
           ID = 905,
            RoomType = "Single",
           Price  = 670.00,
           Capacity = 2
        },
      };

      var hotel_rooms = new Dictionary<int, List<RoomDetails>>()
      {
        {1, richmondRoomsDetails },
        {2, marinaRoomsDetails },
        {3, cabinRoomsDetails }
      };

      return hotel_rooms;
    }
    private IEnumerable<RoomDetails> GetRoomFromHotel(int holetId)
    {
      List<RoomDetails> hotel_rooms = null;
      var rooms = GetAllRooms();
      if (rooms.ContainsKey(holetId))
      {
        hotel_rooms = rooms[holetId];
      }
      return hotel_rooms;
    }
  }
}