using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace Finanzas.Controllers
{
    public class MovimientoController : Controller
    {
        // GET: MovimientoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MovimientoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovimientoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovimientoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovimientoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovimientoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovimientoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovimientoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AgregarMovimiento()
        {
            List<Categoria> listCategorias = new List<Categoria>();
            List<Moneda> listMonedas = null;
            List<SelectListItem> cuentas = new List<SelectListItem>();
            //List<CuentasPorUsuario> listCU = new List<CuentasPorUsuario>();

            using (FinanzasAppContext context = new FinanzasAppContext())
            {
                //listmovimientos = Movimiento.VerMovimientos();
                
                listCategorias = Categoria.VerCategorias();
                listMonedas = Moneda.VerMonedas();
                var user = HttpContext.Session.GetString("UsuarioId");
                //listCU = CuentasPorUsuario.VerCuentasPorUsuario(Convert.ToInt32(user));
                List<SelectListItem> categorias = listCategorias.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Text = c.Nombre,
                        Value = c.Id.ToString(),
                        Selected = false
                    };
                });

                List<SelectListItem> monedas = listMonedas.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Text = c.Nombre,
                        Value = c.Id.ToString(),
                        Selected = false
                    };
                });

                //List<SelectListItem> cu = listCU.ConvertAll(c =>
                //{
                //    return new SelectListItem()
                //    {
                //        Text = Cuenta.BuscarCuentaXNombre(context, c.IdCuenta),
                //        Value = c.IdCuenta.ToString(),
                //        Selected = false
                //    };
                //});
                ViewBag.CU = cuentas;
                ViewBag.SelectListMoneda = monedas;
                ViewBag.Categorias = categorias;
                ViewBag.Monedas = listMonedas;
                //ViewBag.CU = cu;
            }
            return View();

        }

        public ActionResult BuscarCuentas(string moneda)
        {
            List<CuentasPorUsuario> listCU = new List<CuentasPorUsuario>();

            using (FinanzasAppContext context = new FinanzasAppContext())
            {
                var userid = HttpContext.Session.GetString("UsuarioId");
                listCU = CuentasPorUsuario.VerCuentasPorUsuario(Convert.ToInt32(userid),Convert.ToInt32(moneda));
                List<SelectListItem> CU = listCU.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Text = Cuenta.BuscarCuentaXNombre(context, c.IdCuenta),
                        Value = c.IdCuenta.ToString(),
                        Selected = false
                    };
                }).ToList();

                Console.WriteLine(CU.Count);
                return Json(CU);
            }

        }

        public ActionResult VincularMovimiento(string NombreMovimiento, string TipoAccion, string monedaId, string CuentaId, DateTime fecha, string CategoriaId, string Monto)
        {
            FinanzasAppContext context = new FinanzasAppContext();
            var userid = HttpContext.Session.GetString("UsuarioId");
            var CXUId = CuentasPorUsuario.BuscarCXUId(context, Convert.ToInt32(userid), Convert.ToInt32(CuentaId), Convert.ToInt32(monedaId));       

            var Agregado = Movimiento.AgregarMovimiento(context, NombreMovimiento, Convert.ToInt32(TipoAccion), (int)CXUId, fecha, Monto, Convert.ToInt32(CategoriaId));

            if (Agregado)
            {
                var Transaccion = CuentasPorUsuario.HacerMovimiento(context, (int)CXUId, Convert.ToInt32(TipoAccion), Monto);
                if (Transaccion)
                {
                    return Ok(new { message = "Ok" });
                }
                else
                {
                    //Aca se ejecutaria un eliminar movimiento.
                    return BadRequest("error");
                }
            }
            else
            {
                return BadRequest("error");
            }

        }
    }
}