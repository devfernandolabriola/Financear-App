

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
        public ActionResult VincularCuenta([FromForm] VincularCuentaXUsuarioDTO dto)
        {
            var cuentaId = Cuenta.BuscarCuentaId(_context, dto.NombreCuenta);
            if (cuentaId == null)
            {
                Cuenta.AgregarCuenta(_context, dto.NombreCuenta);
                cuentaId = Cuenta.BuscarCuentaId(_context, dto.NombreCuenta);
            }
            var userid = HttpContext.Session.GetString("UsuarioId");
            
            if (string.IsNullOrEmpty(dto.MontoTotal)) dto.MontoTotal = "0";
            
            var asociacion = CuentasPorUsuario.AgregarCuentaAUsuario(_context, Convert.ToInt32(userid), Convert.ToInt32(cuentaId), Convert.ToInt32(dto.Moneda), dto.MontoTotal);

            if (asociacion)
            {
                return Ok(new { message = "Ok" });
            } else
            {
                return BadRequest("error");
            }

        }

        public ActionResult EliminarCuenta()
        {
            ViewBag.TieneCuentas = false;
            bool tieneCuentas = Usuario.VerificarCuentasVinculadasUsuario(_context, int.Parse(HttpContext.Session.GetString("UsuarioId")));
            if(tieneCuentas)
            {
                ViewBag.TieneCuentas = true;
            }
            List<Moneda> listMonedas = null;
            using (FinanzasAppContext context  = new FinanzasAppContext()) {
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
                    ViewBag.Monedas = monedas;
                    ViewBag.CU = monedas;
                    return View();
            }
        }

        public ActionResult BorrarCuenta([FromForm] BorrarCuentaDTO dto)
        {
             FinanzasAppContext context = new FinanzasAppContext();
             var userid = HttpContext.Session.GetString("UsuarioId");
             var CXUId = CuentasPorUsuario.BuscarCXUId(context, Convert.ToInt32(userid), Convert.ToInt32(dto.cuenta), Convert.ToInt32(dto.moneda));

            var Eliminado = CuentasPorUsuario.EliminarCuentaPorUsuario(context, (int)CXUId);

            if(Eliminado)
            {
                return Ok(new { message = "Ok" });
            }
            else
            {
                return BadRequest("error");
            }
        }
    }
}
