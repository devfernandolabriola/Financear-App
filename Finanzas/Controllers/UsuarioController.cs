using Finanzas.DTO;
using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IActionResult Register([FromBody] UsuarioDTO userDTO/*string Email*//*, string Nombre, string Clave*/)
        {
            var resultado = VerificarDatosRegister(userDTO.Email, userDTO.Nombre);
            if (resultado == 422)
            {
                return Unauthorized("Feo");
            }
            var ClaveCifrada = HashHelper.HashPassword(userDTO.Clave);
            var success = Usuario.RegisterUser(_context, userDTO.Email, userDTO.Nombre, ClaveCifrada);
            //if (success) return View("Success"); else return View("Fail");
            if (success)
            {
                var datoUsuario = _context.Usuarios.First(x => x.Email == userDTO.Email);
                return Json(new { success = true, redirect = Url.Action("Index", "Home", new { id = datoUsuario.Id }) });
            };

            return Json(new { success = false, message = "Ocurrió un error al registrar el usuario" });
        }
        
        public int VerificarDatosRegister(string Email, string nombre)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(nombre))
            {
                return 400;
            }
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == Email || x.Nombre == nombre);
            if (usuario != null )
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
