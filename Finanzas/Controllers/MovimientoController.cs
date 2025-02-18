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
            List<Movimiento> listmovimientos = null;
            List<Categoria> listCategorias = null;

            using(FinanzasAppContext context = new FinanzasAppContext())
            {
                var movimiento = new Movimiento();
                listmovimientos = movimiento.VerMovimientos();
                var categoria = new Categoria();
                listCategorias = categoria.VerCategorias();

                List<SelectListItem> movimientos = listmovimientos.ConvertAll(m =>
                {
                    return new SelectListItem()
                    {
                        Text = m.Nombre,
                        Value = m.Nombre,
                        Selected = false
                    };
                });
                List<SelectListItem> categorias = listCategorias.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Text = c.Nombre,
                        Value = c.Nombre,
                        Selected = false
                    };
                });

                ViewBag.Movimiento = movimientos;
                ViewBag.Categorias = categorias;

                return View();
            }

        }
    }
}
