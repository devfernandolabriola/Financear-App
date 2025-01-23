
using Finanzas.Models;
using Finanzas.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Finanzas.Controllers
{
    public class CuentasPorUsuarioController : Controller
    {
        // GET: CuentasPorUsuarioController

        public FinanzasAppContext _context;
       
        public CuentasPorUsuarioController(FinanzasAppContext context)
        {
            
            _context = context;
            
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: CuentasPorUsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CuentasPorUsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentasPorUsuarioController/Create
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

        // GET: CuentasPorUsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CuentasPorUsuarioController/Edit/5
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

        // GET: CuentasPorUsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CuentasPorUsuarioController/Delete/5
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

        public ActionResult AsociarCuenta()
        {
            List<Cuenta> list = null;
            using (FinanzasAppContext context = new FinanzasAppContext())
            {
                    list = (from c in context.Cuentas
                            select new Cuenta
                            {
                                Id = c.Id,
                                Nombre = c.Nombre,
                            }).ToList();
                
            }


            List<SelectListItem> cuentas = list.ConvertAll(c =>
            {
                return new SelectListItem()
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.Cuentas = cuentas;
            return View();

        }

    }
}
