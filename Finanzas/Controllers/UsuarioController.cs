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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] UsuarioDTO userDTO/*string Email*//*, string Nombre, string Clave*/)
        {
            try
            {
                var resultado = Usuario.VerificarDatosRegister(_context, userDTO.Email, userDTO.Nombre);
                if (resultado == 422 || resultado == 400)
                {
                    return Unauthorized("Hubo un error con los datos ingresados.");
                }
                var ClaveCifrada = HashHelper.HashPassword(userDTO.Clave);
                var success = Usuario.RegisterUser(_context, userDTO.Email, userDTO.Nombre, ClaveCifrada);
                //if (success) return View("Success"); else return View("Fail");
                if (success)
                {
                    var datoUsuario = _context.Usuarios.First(x => x.Email == userDTO.Email);
                    SetearContext(datoUsuario.Id, datoUsuario.Nombre);
                    return Ok(new { message = "Ok" });
                };
            } catch( Exception e)
            {
                return BadRequest("Error");
            }
            return BadRequest("Error");
        }
       

        [HttpPost]
        [Route("login")]
        public IActionResult IniciarSesion([FromBody] UsuarioLoginDTO userDTO) 
        {
            var userSearched = Usuario.LoginUser(_context, userDTO.Nombre, userDTO.Clave);
            var success = userSearched != null ? true : false; //Validacion en una linea, si user es distinto de null da true, sino false.
            if (success)
            {
                SetearContext(userSearched.Id, userSearched.Nombre);
                return Ok(new { message = "Ok" });
            }
            else
                return BadRequest("Error");
        }

        private void SetearContext(int Id, string Nombre)
        {
            HttpContext.Session.SetString("UsuarioId", Id.ToString());
            HttpContext.Session.SetString("UsuarioNombre", Nombre);

        }
    }
}
