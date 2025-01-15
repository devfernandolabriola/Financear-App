using Finanzas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Finanzas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected bool IsUserLoggedIn => HttpContext.Session.GetString("UsuarioId") != null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.IsUserLoggedIn = HttpContext.Session.GetString("UsuarioId") != null;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
