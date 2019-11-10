using HotelInterface.DTOs;
using HotelInterface.Interface;
using HotelSystem.Exception;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DummyInMemoryService
{
    public class HotelService : IServiceHotel
    {
        private List<HotelChainIdentifier> hotelList = new List<HotelChainIdentifier>();

        private static Dictionary<int, List<BookingDetails>> bookings = new Dictionary<int, List<BookingDetails>>();

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
            throw new NotImplementedException();
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
                if ((booking  = bookingsForPassport.Find(b => b.Equals(bookingIdentifier))) != null)
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
    }
}
