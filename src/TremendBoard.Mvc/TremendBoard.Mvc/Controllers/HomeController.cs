using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Models;

namespace TremendBoard.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTime _dateTime;
        // Without serilog you have to inject the logger
        // In constructor:
        /*private readonly ILogger _logger;*/

        public HomeController(IDateTime dateTime/*, ILogger logger*/)
        {
            _dateTime = dateTime;
            //_logger = logger;
        }

        // Or directly in the method:
        public IActionResult Index(/*[FromServices] ILogger _logger*/)
        {
            // Basic logger:
            /*_logger.Information("Ana are mere");*/

            // With Serilog you don't have to inject the logger:
            try
            {
                Log.Information("Ana are mere.");
                Log.Warning("Dar are si pere.");
                Log.Error("Ce de fructe are Ama!");
            }
            finally
            {
                Log.CloseAndFlush();
            }

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

            return View();
        }

        public IActionResult About([FromServices] IDateTime dateTime)
        {
            ViewData["Message"] = "Currently on the server the time is " + dateTime.Now;

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
