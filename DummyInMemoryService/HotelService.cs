using HotelInterface.DTOs;
using HotelInterface.Interface;
using HotelSystem.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace DummyInMemoryService
{
  public class HotelService : IServiceHotel
  {
    private List<HotelChainIdentifier> hotelList = new List<HotelChainIdentifier>();

    private static Dictionary<int, List<BookingDetails>> bookings = new Dictionary<int, List<BookingDetails>>();
    private static IQueryable<HotelDetails> GetList()
    {
      var richmondRoomIdentifier = new List<RoomIdentifier>()
          {
            new RoomIdentifier(704)
            {
              ID = 704,
            },
            new RoomIdentifier(705){
              ID = 705,
            },
            new RoomIdentifier(706){
              ID = 706,
            },
          };
      var marinaRoomIdentifier = new List<RoomIdentifier>()
          {
            new RoomIdentifier(1584)
            {
              ID = 1584,
            },
            new RoomIdentifier(1010){
              ID = 1010,
            },
            new RoomIdentifier(1012){
              ID = 1012,
            },
          };
      var cabinRoomIdentifier = new List<RoomIdentifier>()
          {
            new RoomIdentifier(904)
            {
              ID = 904,
            },
          new RoomIdentifier(905){
              ID = 905,
            },
          };

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
        },
         new RoomDetails(101)
        {
            RoomNumber = 101,
            Capacity= 1,
            Price = 150,
            RoomType = "Economy",
        },
        new RoomDetails(102){
            RoomNumber = 102,
            Capacity= 4,
            Price = 300,
            RoomType = "Economy",
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
        },
        new RoomDetails(1023){
           ID = 1023,
            RoomType = "Luxury",
           Price  = 880.00,
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

      var richmond = new HotelDetails(1)
      {
        ID = 1,
        Name = "Richmond Hotel",
        Address = "Vester Farimagsgade 33",
        City = "Copenhagen",
        Rooms = richmondRoomIdentifier,
        StarRating = 3
      };
      var marina = new HotelDetails(2)
      {
        ID = 2,
        Name = "Hotel Marina",
        Address = "Vedbæk Strandvej 391",
        City = "Vedbaek",
        Rooms = marinaRoomIdentifier,
        StarRating = 4,
      };
      var cabin = new HotelDetails(3)
      {
        ID = 3,
        Name = "CABINN Metro Hotel",
        Address = "Arne Jacobsens Allé 2",
        City = "Copenhagen",
        Rooms = cabinRoomIdentifier,
        StarRating = 5,
      };

      var hotels = new List<HotelDetails>();
      hotels.Add(richmond);
      hotels.Add(marina);
      hotels.Add(cabin);

      return hotels.AsQueryable();
    }
    static HotelService()
    {
      bookings.Add(541, new List<BookingDetails>(new BookingDetails[]
      {
                new BookingDetails(5) {
                    StartDate = new DateTime(2020, 1, 15),
                    EndDate = new DateTime(2020, 04, 15),
                    NumberOfRooms = 5,
                    Arrival = new DateTime(2020, 01, 16),
                    ListOfRoomsDetails = new List<RoomDetails>(new RoomDetails[]
                    {
                        new RoomDetails(12)
                        {
                            RoomNumber = 101,
                            Capacity= 1,
                            Price = 150,
                            RoomType = "Economy",
                        },
                        new RoomDetails(51)
                        {
                            RoomNumber = 102,
                            Capacity= 4,
                            Price = 300,
                            RoomType = "Economy",
                        }
                    })
                },
                new BookingDetails(55)
                {
                    StartDate = new DateTime(2020, 6, 15),
                    EndDate = new DateTime(2020, 8, 15),
                    NumberOfRooms = 2,
                    Arrival = new DateTime(2020, 06, 16),
                }
      }));
    }

    public void AddNewHotel(string name, string city, string adress, HotelChainIdentifier hotelChainIdentifier)
    {
      hotelList.Add(hotelChainIdentifier);
    }

    public void CancelBooking(BookingIdentifier bookingIdentifier)
    {
      foreach (var passport in bookings.Keys)
      {
        var passportBookings = bookings[passport];
        var idx = passportBookings.FindIndex(b => b.Equals(bookingIdentifier));
        if (idx != -1)
        {
          passportBookings.RemoveAt(idx);
          return;
        }
      }
      throw new FaultException<BookingNotFoundException>(new BookingNotFoundException());
    }

    public bool CreateBooking(DateTime startDate, DateTime endDate, int numberGuest, List<RoomIdentifier> listOfRooms, int passportNumber, string guestNumber)
    {
      Random random = new Random();
      int ID = random.Next(1000);
      int newKey = random.Next(2000);
      var newBooking = new List<BookingDetails>()
      {
        new BookingDetails(ID){
          StartDate = startDate,
          EndDate = endDate,
          NumberOfRooms = 1,
        },

      };
      bookings.Add(newKey, newBooking);
      return true;
    }

    public string EchoTest(string input)
    {
      return String.Format("Dummy temporary local development service says: {0}", input);
    }

    public BookingDetails FindBookingByid(BookingIdentifier bookingIdentifier)
    {
      foreach (var bookingsForPassport in bookings.Values)
      {
        BookingDetails booking;
        if ((booking = bookingsForPassport.Find(b => b.Equals(bookingIdentifier))) != null)
        {
          return booking;
        }
      }
      return null;
    }

    public List<BookingDetails> FindBookings(int passPortNumber)
    {
      if (bookings.ContainsKey(passPortNumber))
        return bookings[passPortNumber];
      else
        return new List<BookingDetails>();
    }

    public List<RoomDetails> FindRooms(DateTime date, HotelIdentifier hotel, string roomType)
    {
      throw new NotImplementedException();
    }

    public List<HotelDetails> FindAvailableHotels(DateTime startDate, DateTime endDate, int numRooms, string city)
    {
      List<HotelDetails> hotels = GetList().AsQueryable().Where(x => x.City.ToLower() == city.ToLower()).ToList();

      return hotels;
    }

  }
}
