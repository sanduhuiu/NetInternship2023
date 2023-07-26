using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc.Models;

namespace TremendBoard.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTime _dateTime;
        private readonly ITimeService _timeService1;
        private readonly ITimeService _timeService2;


        public HomeController(IDateTime dateTime, ITimeService timeService1, ITimeService timeService2)
        {
            _dateTime = dateTime;
            _timeService1 = timeService1;
            _timeService2 = timeService2;
        }

        public IActionResult Index()
        {
            ViewData["timeService1"] = _timeService1.GetCurrentTime();
            ViewData["timeService2"] = _timeService2.GetCurrentTime();

            var serverTime = _dateTime.Now;
            
            if (serverTime.Hour < 12)
            {
                ViewData["Message"] = "It's morning here - Good Morning!";
            }
            else if (serverTime.Hour < 17)
            {
                ViewData["Message"] = "It's afternoon here - Good Afternoon!";
            }
            else
            {
                ViewData["Message"] = "It's evening here - Good Evening!";
            }

            if(serverTime.Hour < 9 || serverTime.Hour > 17)
            {
                Log.Warning("Index page entered outside business hours");
            } else
            {
                Log.Information("Index page entered normally");
            }

            return View();
        }

        public IActionResult About([FromServices] IDateTime dateTime)
        {
            ViewData["Message"] = "Currently on the server the time is " + dateTime.Now;

            return View();
        }

        public IActionResult Error()
        {
            Log.Error("Error in HomeController");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
