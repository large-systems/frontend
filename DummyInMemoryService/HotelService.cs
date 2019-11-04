using HotelInterface.DTOs;
using HotelInterface.Interface;
using System;
using System.Collections.Generic;

namespace DummyInMemoryService
{
    public class HotelService : IServiceHotel
    {
        private List<HotelChainIdentifier> hotelList = new List<HotelChainIdentifier>();

        private Dictionary<int, List<BookingDetails>> bookings = new Dictionary<int, List<BookingDetails>>();

        public HotelService()
        {
            bookings.Add(541, new List<BookingDetails>(new BookingDetails[]
            {
                new BookingDetails(5) {
                    StartDate = new DateTime(2020, 1, 15),
                    EndDate = new DateTime(2020, 04, 15),
                    NumberOfRooms = 5,
                    Arrival = new DateTime(2020, 01, 16),
                },
                new BookingDetails(5)
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
            throw new NotImplementedException();
        }

        public bool CreateBooking(DateTime startDate, DateTime endDate, int numberGuest, List<RoomIdentifier> listOfRooms, int passportNumber, string guestNumber)
        {
            throw new NotImplementedException();
        }

        public string EchoTest(string input)
        {
            throw new NotImplementedException();
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
