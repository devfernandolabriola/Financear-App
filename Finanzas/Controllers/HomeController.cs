using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Finanzas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected bool IsUserLoggedIn => HttpContext.Session.GetString("UsuarioId") != null;
        public FinanzasAppContext _context;

        public HomeController(ILogger<HomeController> logger, FinanzasAppContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            ViewBag.IsUserLoggedIn = HttpContext.Session.GetString("UsuarioId") != null;
            if(HttpContext.Session.GetString("UsuarioId") != null)
            {
                ViewBag.TieneCuentas = false;
                bool tieneCuentas = Usuario.VerificarCuentasVinculadasUsuario(_context, int.Parse(HttpContext.Session.GetString("UsuarioId")));
                if (tieneCuentas)
                {
                    ViewBag.TieneCuentas = true;
                    ViewBag.MontosPorMoneda = Usuario.DevuelvoMontoTotalXCuenta(_context, int.Parse(HttpContext.Session.GetString("UsuarioId")));
                }
            }
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
