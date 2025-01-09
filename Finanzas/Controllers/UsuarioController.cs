using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(string Email, string Nombre, string Clave)
        {
            var resultado = VerificarCorreo(Email);
            if (resultado == 422)
            {
                return StatusCode(422);
            }
            var ClaveCifrada = HashHelper.HashPassword(Clave);
            var success = Usuario.RegisterUser(_context, Email, Nombre, ClaveCifrada);
            //if (success) return View("Success"); else return View("Fail");
            if (success)
            {
                var datoUsuario = _context.Usuarios.First(x => x.Email == Email);
                return RedirectToAction("Index", "Home", new
                {
                    id = datoUsuario.Id
                });
            }
            return StatusCode(500);
        } 
        

        public int VerificarCorreo(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return 400;
            }
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == Email);
            if (usuario != null)
            {
                return 422;
            }
            return 200;
        }

        [HttpGet]

        public ActionResult IniciarSesion(string Email, string Clave) 
        {
            var User = Usuario.LoginUser(_context, Email, Clave);
            var success = User!=null?true:false; //Validacion en una linea, si user es distinto de null da true, sino false.
            if (success) return View("Success"); else return View("Fail");

        }
    }
}
