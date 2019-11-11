using System.Diagnostics;
using HotelSystem_Frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem_Frontend.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            // TODO log things?
            return View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}