using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSystem_Frontend.Models
{
    public class HotelModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City {get; set;}
        [Required]
        public string Address { get; set; }
        [Required]
        public int HotelChainIdentifier { get; set; }
    }
}
