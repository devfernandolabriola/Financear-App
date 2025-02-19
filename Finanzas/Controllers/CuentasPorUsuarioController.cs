
using Finanzas.DTO;
using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

namespace Finanzas.Controllers
{
    public class CuentasPorUsuarioController : Controller
    {
        // GET: CuentasPorUsuarioController

        public FinanzasAppContext _context;
        protected bool IsUserLoggedIn => HttpContext.Session.GetString("UsuarioId") != null;

        public CuentasPorUsuarioController(FinanzasAppContext context)
        {

            _context = context;

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AsociarCuenta() 
        {
            //Para un dropdownlist, creo una lista null, y usando el context de la base de datos creo el objeto moneda, y hago que la lista sea igual
            //a una lista de monedas llamando a una funcion de la clase moneda
            List<Moneda> listMonedas = null;
            using (FinanzasAppContext context = new FinanzasAppContext())
            {
                listMonedas = Moneda.VerMonedas();

                List<SelectListItem> monedas = listMonedas.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Text = c.Nombre,
                        Value = c.Id.ToString(),
                        Selected = false
                    };
                });

                ViewBag.Monedas = listMonedas;
                ViewBag.ListItemMonedas = monedas;
                return View();
                
            }
        }
        [HttpPost]
        public ActionResult VincularCuenta(string NombreCuenta, string moneda, string MontoTotal)
        {
            Console.WriteLine($"NombreCuenta: {NombreCuenta}, Moneda: {moneda}");
            var cuentaId = Cuenta.BuscarCuentaId(_context, NombreCuenta);
            if (cuentaId == null)
            {
                Cuenta.AgregarCuenta(_context, NombreCuenta);
                cuentaId = Cuenta.BuscarCuentaId(_context, NombreCuenta);
            }
            var userid = HttpContext.Session.GetString("UsuarioId");
            
            if (string.IsNullOrEmpty(MontoTotal)) MontoTotal = "0";
            
            var asociacion = CuentasPorUsuario.AgregarCuentaAUsuario(_context, Convert.ToInt32(userid), Convert.ToInt32(cuentaId), Convert.ToInt32(moneda), MontoTotal);

            if (asociacion)
            {
                return Ok(new { message = "Ok" });
            } else
            {
                return BadRequest("error");
            }

            // Tengo que utilizar el buscarCuenta nuevamente para obtener el id de la cuenta.
            // Tengo que crear un dropdown para que elija la moneda que tendra esa cuenta y una funcion para tomar ese id
            //Una vez tengo esos 3 ids, creo otra funcion en cuentaporusuario que cree la cuenta por usuario y la llamo desde esta funcion.
            //asi ya quedaria asociada la cuenta.





            //List<Cuenta> list = null;
            //using (FinanzasAppContext context = new FinanzasAppContext())
            //{
            //    var cuenta = new Cuenta();
            //    list = cuenta.VerCuentas();

            //    List<SelectListItem> cuentas = list.ConvertAll(c =>
            //    {
            //        return new SelectListItem()
            //        {
            //            Text = c.Nombre,
            //            Value = c.Id.ToString(),
            //            Selected = false
            //        };
            //    });
            //    ViewBag.Cuentas = cuentas;
            //    return View();
            //}

        

            //using (FinanzasAppContext context = new FinanzasAppContext())
            //{
            //    // Obtén las cuentas que coincidan con el prefix (si hay)
            //    var cuentas = context.Cuentas
            //                         .Where(c => c.Nombre.Contains(prefix))  // Filtra por el prefijo
            //                         .Select(c => new { c.Id, c.Nombre })   // Solo selecciona Id y Nombre
            //                         .ToList();

            //    var jsonResponse = JsonConvert.SerializeObject(cuentas);

            //    // Retornamos el JSON serializado
            //    return Content(jsonResponse, "application/json");
            //}



        }
    }
}
