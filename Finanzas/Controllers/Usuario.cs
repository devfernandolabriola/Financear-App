using Finanzas.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finanzas.Controllers
{
    public class Usuario : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string Email, string Nombre, string Clave )
        {

            var usuario = new Models.Usuario();
            usuario.RegisterUser(Email, Nombre, Clave);
            return View("Success");
        }
    }
}
