using HotelInterface.DTOs;
using HotelInterface.Interface;
using HotelSystem.DTOs;
using System;
using System.Collections.Generic;

namespace DummyInMemoryService
{
    public class HotelService : IServiceHotel
    {
        private List<HotelChainIdentifier> hotelList = new List<HotelChainIdentifier>();

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
            throw new NotImplementedException();
        }

        public List<BookingDetails> FindBookings(int passPortNUmber)
        {
            throw new NotImplementedException();
        }

        public List<RoomDetails> FindRooms(DateTime date, HotelIdentifier hotel, string roomType)
        {
            throw new NotImplementedException();
        }
    }
}
