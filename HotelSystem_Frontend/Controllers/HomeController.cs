using HotelInterface.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem_Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceHotel _hotelClient;

        public HomeController(IServiceHotel hotelClient)
        {
            _hotelClient = hotelClient;
        }

        [HttpGet]
        public IActionResult Index([FromQuery]string query = null)
        {
            if (query != null)
            {
                ViewData.Add("response", _hotelClient.EchoTest(query));
            }
            return View();
        }

        

        
    }
}
