using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using HotelInterface.DTOs;

namespace HotelSystem_Frontend.Models
{
    public class HotelDetails
    {
      
            
            public string Name { get; set; }
           
            public string Address { get; set; }
            
            public string City { get; set; }
            
            public float DistanceToCenter { get; set; }
           
            public int StarRating { get; set; }
            
            public List<RoomIdentifier> Rooms { get; set; }

          
     }
    
}
