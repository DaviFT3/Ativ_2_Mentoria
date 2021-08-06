using Ativ_2_Mentoria.Context;
using Ativ_2_Mentoria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ativ_2_Mentoria.Controllers
{
    public class GastoController : Controller
    {
        // GET: GastoController
        public ActionResult Index()
        {
            GastoContext db = new GastoContext();


            var gastos = db.Gastos
                            .Include("Categoria")
                            .ToList();
            var gastosTotais = gastos.Sum(x => x.Valor);
            ViewBag.ValorTotal = gastosTotais;


            return View(gastos);
        }

        // GET: GastoController/Details/5
        public ActionResult Details(int id)
        {
            GastoContext db = new GastoContext();
            var gasto = db.Gastos.Find(id);

            return View(gasto);
        }

        // GET: GastoController/Create
        public ActionResult Create()
        {
            FillCategory(1);
            return View();
        }

        // POST: GastoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gasto obj)
        {
            GastoContext db = new GastoContext();
            if (ModelState.IsValid)
            {
                db.Gastos.Add(obj);
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        // GET: GastoController/Edit/5
        public ActionResult Edit(int id)
        {
            GastoContext db = new GastoContext();
            var gasto = db.Gastos.Find(id);
            FillCategory(gasto.CategoriaId);
            return View(gasto);
        }

        // POST: GastoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Gasto obj)
        {
            GastoContext db = new GastoContext();
            if (ModelState.IsValid)
            {
                using (var dbContext = new GastoContext())
                {
                    Gasto gasto = db.Gastos.First(g => g.Id == id);
                    gasto.Title = obj.Title;
                    dbContext.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }

        // GET: GastoController/Delete/5
        public ActionResult Delete(int id)
        {
            GastoContext db = new GastoContext();
            var gasto = db.Gastos.Find(id);
            return View(gasto);
        }

        // POST: GastoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Gasto obj)
        {
            GastoContext db = new GastoContext();
            if (ModelState.IsValid)
            {
                Gasto gasto = db.Gastos.Find(obj.Id);
                db.Gastos.Remove(gasto);
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }
        public void FillCategory(int id)
        {
            GastoContext db = new GastoContext();

            ViewBag.CategoryId = new SelectList(db.Categorias.ToList(), "IdCategory", "Name", id);

        }
    }
}
