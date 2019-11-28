using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_Recarga.Models;

namespace e_Recarga.Controllers
{
    public class LocalizacaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Localizacao
        public ActionResult Index()
        {
            return View(db.Localizacaos.ToList());
        }

        // GET: Localizacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localizacao localizacao = db.Localizacaos.Find(id);
            if (localizacao == null)
            {
                return HttpNotFound();
            }
            return View(localizacao);
        }

        // GET: Localizacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Localizacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocalizacaoId,Latitude,Longitude,Regiao,Concelho,Freguesia,Localidade")] Localizacao localizacao)
        {
            if (ModelState.IsValid)
            {
                db.Localizacaos.Add(localizacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(localizacao);
        }

        // GET: Localizacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localizacao localizacao = db.Localizacaos.Find(id);
            if (localizacao == null)
            {
                return HttpNotFound();
            }
            return View(localizacao);
        }

        // POST: Localizacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocalizacaoId,Latitude,Longitude,Regiao,Concelho,Freguesia,Localidade")] Localizacao localizacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localizacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localizacao);
        }

        // GET: Localizacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localizacao localizacao = db.Localizacaos.Find(id);
            if (localizacao == null)
            {
                return HttpNotFound();
            }
            return View(localizacao);
        }

        // POST: Localizacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localizacao localizacao = db.Localizacaos.Find(id);
            db.Localizacaos.Remove(localizacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
