using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            List<CuentasPorUsuario> listCU = new List<CuentasPorUsuario>();

            using (FinanzasAppContext context = new FinanzasAppContext())
            {
                //listmovimientos = Movimiento.VerMovimientos();
                listCategorias = Categoria.VerCategorias();
                var user = HttpContext.Session.GetString("UsuarioId");
                listCU = CuentasPorUsuario.VerCuentasPorUsuario(Convert.ToInt32(user));
                List<SelectListItem> categorias = listCategorias.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Text = c.Nombre,
                        Value = c.Id.ToString(),
                        Selected = false
                    };
                });

                List<SelectListItem> cu = listCU.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Text = Cuenta.BuscarCuentaXNombre(context, c.IdCuenta),
                        Value = c.IdCuenta.ToString(),
                        Selected = false
                    };
                });

                ViewBag.Categorias = categorias;
                ViewBag.CU = cu;
            }
            return View();

        }
    }
}
