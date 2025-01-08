using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finanzas.Controllers
{
    public class UsuarioController : Controller
    {
        public FinanzasAppContext _context;

        public UsuarioController(FinanzasAppContext context)
        {
            _context = context;
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string Email, string Nombre, string Clave )
        {
            var ClaveCifrada = HashHelper.HashPassword( Clave );
            var success = Usuario.RegisterUser(_context, Email, Nombre, ClaveCifrada);
            if(success) return View("Success"); else return View("Fail");
        }
    }
}
