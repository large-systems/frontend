using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceProxy;

namespace HotelSystem_Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly HotelServiceClient _hotelClient;

        public HomeController(HotelServiceClient hotelClient)
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
