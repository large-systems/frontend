using HotelInterface.DTOs;
using HotelInterface.Interface;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ServiceProxy
{
    public class HotelServiceClient : ClientBase<IServiceHotel>, IServiceHotel
    {

        public HotelServiceClient(Binding binding, EndpointAddress address) : base(binding, address) { }
        
        public void AddNewHotel(string name, string city, string adress, HotelChainIdentifier hotelChainIdentifier)
        {
            Channel.AddNewHotel(name, city, adress, hotelChainIdentifier);
        }

        public void CancelBooking(BookingIdentifier bookingIdentifier)
        {
            Channel.CancelBooking(bookingIdentifier);
        }

        public bool CreateBooking(DateTime startDate, DateTime endDate, int numberGuest, List<RoomIdentifier> listOfRooms, int passportNumber, string guestNumber)
        {
            return Channel.CreateBooking(startDate, endDate, numberGuest, listOfRooms, passportNumber, guestNumber);
        }

        public string EchoTest(string input)
        {
            return Channel.EchoTest(input);
        }

        public List<HotelDetails> FindAvailableHotels(DateTime startDate, DateTime endDate, int numRooms, string city)
        {
            return Channel.FindAvailableHotels(startDate, endDate, numRooms, city);
        }

        public BookingDetails FindBookingByid(BookingIdentifier bookingIdentifier)
        {
            return Channel.FindBookingByid(bookingIdentifier);
        }

        public List<BookingDetails> FindBookings(int passPortNUmber)
        {
            return Channel.FindBookings(passPortNUmber);
        }

        public List<RoomDetails> FindRooms(HotelIdentifier hotel, DateTime startDate, DateTime endDate, string roomType)
        {
            return Channel.FindRooms(hotel, startDate, endDate, roomType);
        }
    }
}
